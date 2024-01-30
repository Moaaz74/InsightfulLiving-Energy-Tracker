package Demo;

import org.apache.kafka.clients.producer.ProducerConfig;
import org.apache.log4j.Level;
import org.apache.log4j.Logger;
import org.apache.spark.sql.*;
import org.apache.spark.sql.streaming.StreamingQueryException;
import org.apache.spark.sql.types.DataTypes;

import java.util.Properties;
import java.util.concurrent.TimeoutException;

import static org.apache.spark.sql.functions.*;

public class ElectricityProcessing {
    public static void main(String[] args){
        System.setProperty("hadoop.home.dir", "c:/hadoop");
        Logger.getLogger("org.apache").setLevel(Level.WARN);
        Logger.getLogger("org.apache.spark.storage").setLevel(Level.ERROR);

        SparkSession spark = SparkSession.builder()
                .appName("EnergyConsumptionAnalysis")
                .master("local[*]")
                .getOrCreate();
        Properties producerProps = new Properties();
        producerProps.put(ProducerConfig.BOOTSTRAP_SERVERS_CONFIG, "localhost:9092");
        producerProps.put(ProducerConfig.KEY_SERIALIZER_CLASS_CONFIG, "org.apache.kafka.common.serialization.StringSerializer");
        producerProps.put(ProducerConfig.VALUE_SERIALIZER_CLASS_CONFIG, "org.apache.kafka.common.serialization.StringSerializer");

        Dataset<Row> jsonStream = spark.readStream()
                .format("kafka")
                .option("kafka.bootstrap.servers", "localhost:9092")
                .option("subscribe", "simulatorTopic")
                .option("startingOffsets", "latest")
                .option("failOnDataLoss", "false")
                .load()
                .selectExpr("CAST(value AS STRING)");

        Dataset<Row> ResultStream = jsonStream.selectExpr("CAST(value AS STRING)")
                .select(from_json(col("value"), getSchema()).as("data"))
                .select("data.*")
                .withColumn("SensorId", col("SensorId").cast(DataTypes.IntegerType))
                .withColumn("HomeId", col("HomeId").cast(DataTypes.IntegerType))
                .withColumn("DateTime", col("DateTime").cast(DataTypes.TimestampType))
                .withColumn("Type", col("Type"))
                .withColumn("DayOfWeek" ,functions.date_format(col("DateTime")  /*functions.date_format(functions.current_timestamp()*/ , "y-M"))
                .withColumn("Day", functions.date_format(col("DateTime")  /*functions.date_format(functions.current_timestamp()*/ , "yyyy-MM-dd"))
                .filter("Type == 'Electricity'")
                .withColumn("Value" , col("Value").divide(1000.0))
                .groupBy(functions.col("HomeId"), functions.col("SensorId") , col("Day") , functions.col("DayOfWeek").alias("Month"))
                .agg(sum("Value").alias("NetConsumption"))
                .selectExpr("to_json(struct(*)) as json_data");;


        try {
            try {
                ResultStream
                        .selectExpr("CAST(json_data AS STRING) as value")
                        .writeStream()
                        .outputMode("update")
                        .format("kafka")
                        .option("topic" , "ElectricityConsumption")
                        //.option("truncate" , false)
                        .option("checkpointLocation" , "C:\\tmp\\data1")
                        .option("kafka.bootstrap.servers", "localhost:9092")
                        .start().awaitTermination();
            } catch (StreamingQueryException e) {
                throw new RuntimeException(e);
            }
        } catch (TimeoutException e) {
            throw new RuntimeException(e);
        }


    }
    private static org.apache.spark.sql.types.StructType getSchema() {
        return new org.apache.spark.sql.types.StructType()
                .add("SensorId", DataTypes.StringType)
                .add("HomeId", DataTypes.StringType)
                .add("DateTime", DataTypes.StringType)
                .add("Type", DataTypes.StringType)
                .add("Value", DataTypes.StringType);
    }
}
