package TP1;


import java.io.*;
import java.util.zip.DeflaterOutputStream;
import java.util.zip.InflaterInputStream;

public class Writer {
    private static Writer instance;

    static Writer getInstance() {
        if (instance == null) {
            instance = new Writer();
        }
        return instance;
    }

    void ecrireFichierCompresse(String path, byte[] enTete, byte[] texte) {
        try {
            BufferedOutputStream outputStream = new BufferedOutputStream(new FileOutputStream(path));
            if (enTete != null)
                outputStream.write(enTete);
            outputStream.write(texte);
            outputStream.flush();
            outputStream.close();
        } catch (IOException e) {
            System.err.println(e.getMessage());
        }
    }

    void ecrireFichierDecompresse(String path, byte[] body) {
        try {
            OutputStream out = new FileOutputStream(path);
            out.write(body);
            out.close();
        } catch (IOException e) {
            System.err.println(e.getMessage());
        }
    }

    void ecrireFichierOptimiseCompresse(String entree, String sortie) {

        try {
            FileInputStream fis = new FileInputStream(entree);
            FileOutputStream fos = new FileOutputStream(sortie);
            DeflaterOutputStream dos = new DeflaterOutputStream(fos);

            Optimise.getInstance().copier(fis, dos);
        } catch (FileNotFoundException e) {
            System.err.println(e.getMessage());
        }
    }

    void ecrireFichierOptimiseDecompresse(String entree, String sortie) {
        try {
            FileInputStream fis2 = new FileInputStream(entree);
            InflaterInputStream iis = new InflaterInputStream(fis2);
            FileOutputStream fos2 = new FileOutputStream(sortie);

            Optimise.getInstance().copier(iis, fos2);
        } catch (FileNotFoundException e) {
            System.err.println(e.getMessage());
        }
    }

}
