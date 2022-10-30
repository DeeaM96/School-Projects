package TP1;

import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;

public class Optimise {
    private static Optimise instance;

    public static Optimise getInstance() {
        if (instance == null) {
            instance = new Optimise();
        }

        return instance;
    }

    public void CompressionOptimise(String pathFichierEntree, String pathFichierSortie) throws IOException {
        pathFichierSortie = changerNomSortie(pathFichierEntree, pathFichierSortie);

        Writer.getInstance().ecrireFichierOptimiseCompresse(pathFichierEntree, pathFichierSortie);
    }

    public void DecompressionOptimise(String pathFichierEntree, String pathFichierSortie) throws IOException {
        pathFichierSortie = changerNomSortie(pathFichierEntree, pathFichierSortie);

        Writer.getInstance().ecrireFichierOptimiseDecompresse(pathFichierEntree, pathFichierSortie);
    }

    public String changerNomSortie(String pathFichierEntree, String pathFichierSortie) {
        if (pathFichierEntree.equals(pathFichierSortie)) {
            File f = new File(pathFichierSortie);
            if (f.getName().contains(".")) {
                String name = f.getName();
                String[] tmp = name.split("\\.");
                pathFichierSortie = pathFichierSortie.replace(name, tmp[0] + "_inflate." + tmp[tmp.length - 1]);
            } else
                pathFichierSortie += "_inflate";
        }
        return pathFichierSortie;
    }

    public void copier(InputStream is, OutputStream os) {
        int oneByte;

        try {
            while ((oneByte = is.read()) != -1) {
                os.write(oneByte);
            }
            os.close();
            is.close();
        } catch (Exception e) {
            System.err.println(e.getMessage());
        }
    }
}
