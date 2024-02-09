package Demo;

import org.apache.kafka.clients.producer.ProducerConfig;
import org.apache.log4j.Level;
import org.apache.log4j.Logger;
import org.apache.spark.sql.Dataset;
import org.apache.spark.sql.Row;
import org.apache.spark.sql.SparkSession;
import org.apache.spark.sql.streaming.StreamingQueryException;
import org.apache.spark.sql.types.DataTypes;

import java.util.Properties;
import java.util.concurrent.TimeoutException;

import static org.apache.spark.sql.functions.*;

public class RoomProcessing {
    public static void main(String[] args) {
        System.setProperty("hadoop.home.dir", "c:/hadoop");
        Logger.getLogger("org.apache").setLevel(Level.WARN);
        Logger.getLogger("org.apache.spark.storage").setLevel(Level.ERROR);

        SparkSession spark = SparkSession.builder()
                .appName("EnergyTracker")
                .master("local[*]")
                .getOrCreate();
        Properties producerProps = new Properties();
        producerProps.put(ProducerConfig.BOOTSTRAP_SERVERS_CONFIG, "localhost:9092");
        producerProps.put(ProducerConfig.KEY_SERIALIZER_CLASS_CONFIG, "org.apache.kafka.common.serialization.StringSerializer");
        producerProps.put(ProducerConfig.VALUE_SERIALIZER_CLASS_CONFIG, "org.apache.kafka.common.serialization.StringSerializer");

        Dataset<Row> jsonStream = spark.readStream()
                .format("kafka")
                .option("kafka.bootstrap.servers", "localhost:9092")
                .option("subscribe", "nifi1")
                .option("startingOffsets", "latest")
                .option("failOnDataLoss", "false")
                .load()
                .selectExpr("CAST(value AS STRING)");

        Dataset<Row> resultStream = jsonStream.selectExpr("CAST(value AS STRING)")
                .select(from_json(col("value"), getSchema()).as("data"))
                .select("data.*")
                .withColumn("RoomId", col("RoomId").cast(DataTypes.IntegerType))
                .withColumn("DateTime", col("DateTime").cast(DataTypes.TimestampType))
                .withColumn("EnergyType", col("EnergyType"))
                .withColumn("Value", col("Value").cast(DataTypes.DoubleType))
                .withColumn("window" , window(col("DateTime") , "1 hour"))
                .groupBy(col("RoomId") , col("window") , col("EnergyType"))
                .agg(sum("Value").alias("RoomConsumption"))
                .select("RoomId" , "window.start" , "window.end" , "RoomConsumption" , "EnergyType")
                .selectExpr("to_json(struct(*)) as json_data");

        try {
            try {
                resultStream
                        .selectExpr("CAST(json_data AS STRING) as value")
                        .writeStream()
                        .outputMode("update")
                        .format("kafka")
//                        .option("truncate" , false)
                        .option("topic", "ElectricityConsumption")
                        .option("checkpointLocation", "C:\\tmp\\data1")
                        .option("kafka.bootstrap.servers", "localhost:9092")
                        .start()
                        .awaitTermination();
            } catch (StreamingQueryException e) {
                throw new RuntimeException(e);
            }
        } catch (TimeoutException e) {
            throw new RuntimeException(e);
        }
    }

    private static org.apache.spark.sql.types.StructType getSchema() {
        return new org.apache.spark.sql.types.StructType()
                .add("HomeId" , DataTypes.StringType)
                .add("RoomId" , DataTypes.StringType)
                .add("ApplianceId", DataTypes.StringType)
                .add("DateTime", DataTypes.StringType)
                .add("EnergyType", DataTypes.StringType)
                .add("Value", DataTypes.StringType);
    }
}
