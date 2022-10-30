package TP1;

import java.io.BufferedInputStream;
import java.io.File;
import java.io.FileInputStream;

public class Byte_Compare {
    public static void main(String[] args) {
        String fichier_1 = "src/carte.png", fichier_2 = "src/carte_decomp.png";
        //String fichier_1 = "src/gael.jpg", fichier_2 = "src/gael_decomp.jpg";
        //String fichier_1 = "src/exemple.txt", fichier_2 = "src/d_exemple.txt";
        //String fichier_1 = "src/test.txt", fichier_2 = "src/d_test.txt";

        byte[] valeur_1 = new byte[1], valeur_2 = new byte[1];
        int diffCount = 0;
        System.out.println("Commencement - comparaison d'octets entre " + fichier_1 + " et " + fichier_2);
        try (BufferedInputStream input_1 = new BufferedInputStream(new FileInputStream(new File(fichier_1)))) {
            BufferedInputStream input_2 = new BufferedInputStream(new FileInputStream(new File(fichier_2)));
            while ((input_1.read(valeur_1)) != -1) {
                if ((input_2.read(valeur_2)) != -1) {
                    if (valeur_1[0] != valeur_2[0]) {
                        System.out.println(valeur_1[0] + " " + new String(valeur_1)
                                + " " + valeur_2[0] + " " + new String(valeur_2));
                        diffCount++;
                    }
                } else {
                    break;
                }
            }
            input_1.close();
            input_2.close();
            System.out.println("Fin. Nb de differences : " + diffCount);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
