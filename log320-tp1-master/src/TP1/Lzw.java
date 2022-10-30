package TP1;

import java.io.*;
import java.util.Arrays;
import java.util.HashMap;


public class Lzw {

    private static Lzw instance;

    private Lzw() {
    }

    static Lzw getInstance() {
        if (instance == null) {
            instance = new Lzw();
        }

        return instance;
    }

    public void CompressionLZW(String input, String output) throws IOException {
        int i;
        //String input = "src/carte.png", output = "src/carte_comp.lzw";
        //String input = "src/gael.jpg", output = "src/gael_comp.lzw";
        //String input = "src/exemple.txt", output = "src/c_exemple.txt";
        //String input = "src/test.txt", output = "src/c_test.txt";


        HashMap<ByteArray, Integer> dict = new HashMap<ByteArray, Integer>();
        for (i = 0; i < 256; i++) {
            dict.put(new ByteArray(new byte[]{(byte) i}), i);
            //System.out.println(i + " " + (byte)i + " " + new String(new byte[]{(byte)i}));
        }

        //String symbole = null, nouveauSymbole, byteEnString;
        ByteArray symbole, nouveauSymbole;
        byte[] valeurLue = new byte[1];
        try (BufferedInputStream inputStream = new BufferedInputStream(new FileInputStream(new File(input)))) {
            if ((inputStream.read(valeurLue)) != -1) { // Gere si fichier vide
                BufferedOutputStream outputStream = new BufferedOutputStream(new FileOutputStream(new File(output)));
                symbole = new ByteArray(valeurLue.clone());
                while ((inputStream.read(valeurLue)) != -1) {
                    nouveauSymbole = new ByteArray(Arrays.copyOf(symbole.getArray(), symbole.length() + 1));
                    nouveauSymbole.getArray()[symbole.length()] = valeurLue[0];
                    if (dict.containsKey(nouveauSymbole)) {
                        symbole = nouveauSymbole;
                    } else {
                        ecrireCode(dict, symbole, outputStream);
                        if (i > 65536) {
                            dict.clear();
                            for (i = 0; i < 256; i++)
                                dict.put(new ByteArray(new byte[]{(byte) i}), i);
                        }
                        dict.put(nouveauSymbole, i);
                        //System.out.println(i + " " + nouveauSymbole);
                        symbole = new ByteArray(valeurLue.clone());
                        i++;
                    }
                }
                ecrireCode(dict, symbole, outputStream);
                outputStream.close();
            }
            inputStream.close();
        } catch (Exception e) {
            e.printStackTrace();
        }


    }

    private static void ecrireCode(HashMap<ByteArray, Integer> dict, ByteArray symbole, BufferedOutputStream outputStream) {
        try {
            int codeSortie = dict.get(symbole);
            outputStream.write(new byte[]{(byte) (codeSortie / 256), (byte) (codeSortie % 256)});
            //System.out.println(symbole.toString() + " " + codeSortie);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void DecompressionLZW(String input, String output) throws IOException {
        int i, valeurLue;
        byte[] valeursLues = new byte[2];
        //String input = "src/carte_comp.lzw", output = "src/carte_decomp.png";
        //String input = "src/gael_comp.lzw", output = "src/gael_decomp.jpg";
        //String input = "src/c_exemple.txt", output = "src/d_exemple.txt";
        //String input = "src/c_test.txt", output = "src/d_test.txt";

        byte[] symbole = null, nouveauSymbole, dictSymbole;
        HashMap<Integer, byte[]> dict = new HashMap<Integer, byte[]>();
        for (i = 0; i < 256; i++)
            dict.put(i, new byte[]{(byte) i});

        try (BufferedInputStream inputStream = new BufferedInputStream(new FileInputStream(new File(input)))) {
            BufferedOutputStream outputStream = new BufferedOutputStream(new FileOutputStream(new File(output)));
            while (inputStream.read(valeursLues) != -1) {
                valeurLue = 256 * (int) (valeursLues[0] & 0xFF) + (int) (valeursLues[1] & 0xFF);
                if (valeurLue >= i) {
                    nouveauSymbole = Arrays.copyOf(symbole, symbole.length + 1);
                    nouveauSymbole[symbole.length] = symbole[0];
                } else {
                    nouveauSymbole = dict.get(valeurLue);
                }
                outputStream.write(nouveauSymbole);
                //System.out.print(valeurLue + " " + nouveauSymbole);
                if (symbole != null) {
                    if (i > 65536) {
                        dict.clear();
                        for (i = 0; i < 256; i++)
                            dict.put(i, new byte[]{(byte) i});
                    }
                    dictSymbole = Arrays.copyOf(symbole, symbole.length + 1);
                    dictSymbole[symbole.length] = nouveauSymbole[0];
                    dict.put(i, dictSymbole);
                    //System.out.println(i + new String(dictSymbole));
                    i++;
                }
                symbole = nouveauSymbole;
            }
            inputStream.close();
            outputStream.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

}

