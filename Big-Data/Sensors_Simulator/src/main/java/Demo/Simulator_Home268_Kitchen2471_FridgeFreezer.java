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

public class Simulator_Home268_Kitchen2471_FridgeFreezer {
    public static void main(String[] args) throws TimeoutException, StreamingQueryException {
        System.setProperty("hadoop.home.dir", "c:/hadoop");
        Logger.getLogger("org.apache").setLevel(Level.WARN);
        Logger.getLogger("org.apache.spark.storage").setLevel(Level.ERROR);

        Properties properties = new Properties();
        properties.put("bootstrap.servers", "localhost:9092");
        properties.put("key.serializer", "org.apache.kafka.common.serialization.StringSerializer");
        properties.put("value.serializer", "org.apache.kafka.common.serialization.StringSerializer");

        Producer<String, String> producer = new KafkaProducer<>(properties);

        try (BufferedReader br = new BufferedReader(new FileReader("C:\\Users\\moaaz\\OneDrive\\Desktop\\Homes\\Home 268\\home268-elec\\home268_kitchen2471_sensor21533_electric-appliance_fridgefreezer.csv"))) {
            String line;
            boolean f = true;

            while ((line = br.readLine()) != null) {

                if(f){f=false;continue;}

                String topic = "nifi1";
                String jsonRecord = convertToJSON(line);

                producer.send(new ProducerRecord<>(topic, jsonRecord));
                System.out.println(jsonRecord);

               Thread.sleep(5000);
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
        int roomId = 2471;
        int applianceId = 4094;
        Date currentDate = new Date();
        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        String formattedDate = sdf.format(currentDate);
        String type = "Electricity";
        return "{\"ApplianceId\" : \"" + applianceId + "\" , \"DateTime\": \"" + formattedDate + "\" , \"EnergyType\": \"" + type +  "\", \"Value\":\"" + sentAttributes[1] + "\"}";
    }

}
