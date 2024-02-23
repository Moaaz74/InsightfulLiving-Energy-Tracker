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
import java.util.Calendar;
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

        try (BufferedReader br = new BufferedReader(new FileReader("C:\\Users\\Moataz Nasr\\OneDrive - Faculty of Computers and Information\\Desktop\\home268_kitchen2471_sensor21533_electric-appliance_fridgefreezer.csv\\home268_kitchen2471_sensor21533_electric-appliance_fridgefreezer.csv"))) {
            String line;
            boolean f = true;
            int i =1;
            Date time ;
            Calendar calendar = Calendar.getInstance();
            while ((line = br.readLine()) != null) {

                if(f){f=false;continue;}

                String topic = "nifi1";
                Calendar clonedCalendar = (Calendar) calendar.clone();
                if(i==3) {
                    clonedCalendar.add(Calendar.HOUR_OF_DAY, 1);
                    i = 1;
                }
                else {
                    i++;
                }
                time = clonedCalendar.getTime();
                calendar = clonedCalendar;
                String jsonRecord = convertToJSON(line,time);

                producer.send(new ProducerRecord<>(topic, jsonRecord));
                System.out.println(jsonRecord);

               Thread.sleep(15000);
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

    private static String convertToJSON(String line,Date time) {
        String[] sentAttributes = line.split(",");
        int homeId = 268;
        int roomId = 2471;
        int applianceId = 4094;
        Date currentDate = new Date();
        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        String formattedDate = sdf.format(time);
        String type = "Electricity";
        return "{\"HomeId\" : \"" + homeId + "\" , \"RoomId\" : \""+ roomId + "\" , \"ApplianceId\" : \"" + applianceId + "\" , \"DateTime\": \"" + formattedDate + "\" , \"EnergyType\": \"" + type +  "\", \"Value\":\"" + sentAttributes[1] + "\"}";
    }

}
