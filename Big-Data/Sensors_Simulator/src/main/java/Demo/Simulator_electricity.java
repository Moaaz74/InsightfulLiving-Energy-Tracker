package Demo;
import org.apache.kafka.clients.producer.KafkaProducer;
import org.apache.kafka.clients.producer.Producer;
import org.apache.kafka.clients.producer.ProducerRecord;
import org.apache.log4j.Level;
import org.apache.log4j.Logger;
import org.apache.spark.sql.streaming.StreamingQueryException;


import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Properties;
import java.util.concurrent.TimeoutException;

public class Simulator_electricity {

    public static void main(String[] args) throws TimeoutException, StreamingQueryException {
        System.setProperty("hadoop.home.dir", "c:/hadoop");
        Logger.getLogger("org.apache").setLevel(Level.WARN);
        Logger.getLogger("org.apache.spark.storage").setLevel(Level.ERROR);

        Properties properties = new Properties();
        properties.put("bootstrap.servers", "localhost:9092");
        properties.put("key.serializer", "org.apache.kafka.common.serialization.StringSerializer");
        properties.put("value.serializer", "org.apache.kafka.common.serialization.StringSerializer");

        Producer<String, String> producer = new KafkaProducer<>(properties);

        try (BufferedReader br = new BufferedReader(new FileReader("C:\\Users\\Moataz Nasr\\OneDrive - Faculty of Computers and Information\\Desktop\\home 80\\home80_hall873_sensor3068c3079_electric-mains_electric-combined.csv\\home80_hall873_sensor3068c3079_electric-mains_electric-combined.csv"))) {
            String line;
            boolean f = true;

            while ((line = br.readLine()) != null) {

                if(f){f=false;continue;}

                String topic = "nifi1";
                String jsonRecord = convertToJSON(line);

                producer.send(new ProducerRecord<>(topic, jsonRecord));
                System.out.println(jsonRecord);

                Thread.sleep(30000);
            }
        } catch (FileNotFoundException e) {
            throw new RuntimeException(e);
        } catch (IOException e) {
            throw new RuntimeException(e);
        } catch (InterruptedException e) {
            throw new RuntimeException(e);
        } finally {
            producer.close();
        }



    }

    private static String convertToJSON(String line) {
        String[] sentAttributes = line.split(",");

        Date currentDate = new Date();
        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        String formattedDate = sdf.format(currentDate);
        int  sensorid = 3068;
        String type = "Electricity";
        return "{\"Sensorid\": \"" + sensorid + "\" , \"DateTime\": \"" + formattedDate + "\" , \"Type\": \"" + type +  "\", \"Value\":\"" + sentAttributes[1] + "\"}";
    }
}