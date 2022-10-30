package TP1;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;

public class Reader {

    private static Reader instance;

    static Reader getInstance() {
        if (instance == null) {
            instance = new Reader();
        }

        return instance;
    }

    byte[] readFile(String path) throws FileNotFoundException, IOException {
        File file = new File(path);
        byte[] encodedFileContent = new byte[(int) file.length()];

        int result = new FileInputStream(file).read(encodedFileContent);
        if (result == 0)
            return null;


        return encodedFileContent;
    }
}
