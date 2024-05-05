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
import java.text.DecimalFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Properties;
import java.util.concurrent.TimeoutException;

public class Simulator_temp_humidity {
    public static void main(String[] args) throws TimeoutException, StreamingQueryException {
        System.setProperty("hadoop.home.dir", "c:/hadoop");
        Logger.getLogger("org.apache").setLevel(Level.WARN);
        Logger.getLogger("org.apache.spark.storage").setLevel(Level.ERROR);

        Properties properties = new Properties();
        properties.put("bootstrap.servers", "localhost:9092");
        properties.put("key.serializer", "org.apache.kafka.common.serialization.StringSerializer");
        properties.put("value.serializer", "org.apache.kafka.common.serialization.StringSerializer");

        Producer<String, String> producer = new KafkaProducer<>(properties);

        try (BufferedReader br = new BufferedReader(new FileReader("C:\\Users\\Moataz Nasr\\OneDrive - Faculty of Computers and Information\\Desktop\\home 80\\temp&humidity_Home80_Room876.csv"))){
            String line;
            boolean f = true;

            while ((line = br.readLine()) != null) {

                if(f){f=false;continue;}

                String topic = "nifitemp-humidity";
                String jsonRecord = convertToJSON(line);

                producer.send(new ProducerRecord<>(topic, jsonRecord));
                System.out.println(jsonRecord);

                Thread.sleep(70000);
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
        int  roomId =2471 ;
        double temp;
        DecimalFormat df = new DecimalFormat("0.00");;
        if(Double.parseDouble(sentAttributes[1])==0.0){
            temp    = (Math.random() * 50);
            sentAttributes[1] = String.valueOf( df.format(temp) );

        }
        else
        {
            temp = Double.parseDouble( sentAttributes[1]);
            temp *=0.05;
            sentAttributes[1] =String.valueOf(df.format( temp ));
        }
        temp = Double.parseDouble( sentAttributes[2]);
        temp *=0.09;
        sentAttributes[2] =String.valueOf(df.format(temp ));

        return "{\"RoomId\": \"" + roomId + "\" , \"DateTime\": \"" + formattedDate + "\" , \"Temperature\": \"" + sentAttributes[1] +  "\" , \"Humidity\":\"" + sentAttributes[2] + "\"}";
    }
}
