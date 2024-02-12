package Demo;
import org.apache.kafka.clients.producer.KafkaProducer;
import org.apache.kafka.clients.producer.Producer;
import org.apache.kafka.clients.producer.ProducerConfig;
import org.apache.kafka.clients.producer.ProducerRecord;

import java.io.IOException;
import java.nio.file.*;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Properties;

public class SimulatorImages {

    public static void main(String[] args) {
        // Kafka Producer Configuration
        Properties properties = new Properties();
        properties.put(ProducerConfig.BOOTSTRAP_SERVERS_CONFIG, "localhost:9092");
        properties.put(ProducerConfig.KEY_SERIALIZER_CLASS_CONFIG, "org.apache.kafka.common.serialization.StringSerializer");
        properties.put(ProducerConfig.VALUE_SERIALIZER_CLASS_CONFIG, "org.apache.kafka.common.serialization.StringSerializer");

        // Create Kafka producer
        Producer<String, String> producer = new KafkaProducer<>(properties);

        // Directory to monitor
        String directoryToMonitor = "C:\\Users\\Moataz Nasr\\Downloads\\images";

        try {
            // Create a WatchService
            WatchService watchService = FileSystems.getDefault().newWatchService();
            Path path = Paths.get(directoryToMonitor);
            path.register(watchService, StandardWatchEventKinds.ENTRY_CREATE);

            // Infinite loop to monitor the directory
            while (true) {
                WatchKey key = watchService.take();

                for (WatchEvent<?> event : key.pollEvents()) {
                    if (event.kind() == StandardWatchEventKinds.ENTRY_CREATE) {
                        // A new file has been created, get its path and send to Kafka
                        Path newPath = (Path) event.context();
                        String photoPath = path.resolve(newPath).toString();

                        producer.send(new ProducerRecord<>("photo-topic",convertToJSON(photoPath)));
                    }
                }

                // Reset the key
                boolean valid = key.reset();
                if (!valid) {
                    System.out.println("WatchKey no longer valid, exiting monitoring.");
                    break;
                }
            }

        } catch (IOException | InterruptedException e) {
            e.printStackTrace();
        } finally {
            // Close the Kafka producer
            producer.close();
        }
    }

    private static String convertToJSON(String path) {
        int roomId = 2471;
        int homeId = 268;

        Date currentDate = new Date();
        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        String formattedDate = sdf.format(currentDate);
        path = path.replace("\\","\\\\");
        return "{\"HomeId\" : \"" + homeId + "\" , \"RoomId\" : \"" + roomId + "\" , \"DateTime\": \"" + formattedDate + "\" , \"ImagePath\" : \"" + path + "\"}";
    }
}
