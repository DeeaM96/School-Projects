package TP1;

import java.io.IOException;

public class Main {

    final static String HUFFMAN = "-huff";
    final static String LZW = "-lzw";
    final static String OPTIMISE = "-opt";
    final static String COMPRESS = "-c";
    final static String DECOMPRESS = "-d";
    static String pathFichierEntree;
    static String pathFichierSortie;

    public static void main(String[] args) throws IOException {
/*
        args = new String[4];
        args[0] = OPTIMISE;
        args[1] = DECOMPRESS;

        args[2] = "src/TP1/com.txt";
        args[3] = "src/TP1/decom.txt"; */

        long tempsDebut = System.currentTimeMillis();
        if (args.length >= 4) {
            Boolean compression = args[1].equals(COMPRESS);
            Boolean decompression = args[1].equals(DECOMPRESS);
            pathFichierEntree = args[2];
            pathFichierSortie = args[3];
            if (compression) {
                choisirTypeCompression(args[0]);
            }
            if (decompression) {
                choisirTypeDecompression(args[0]);
            }
        }

        long tempsFin = System.currentTimeMillis();
        System.out.println("Execution time : " + (tempsDebut - tempsFin) + " milliseconds");

    }

    private static void choisirTypeCompression(String type) throws IOException {
        switch (type) {
            case HUFFMAN:
                System.out.println("-huff -c " + pathFichierEntree + " " + pathFichierSortie);
                Huffman.getInstance().CompressionHuffman(pathFichierEntree, pathFichierSortie);
                break;
            case LZW:
                System.out.println("-lzw -c " + pathFichierEntree + " " + pathFichierSortie);
                Lzw.getInstance().CompressionLZW(pathFichierEntree, pathFichierSortie);
                break;
            case OPTIMISE:
                System.out.println("-opt -c " + pathFichierEntree + " " + pathFichierSortie);
                Optimise.getInstance().CompressionOptimise(pathFichierEntree, pathFichierSortie);
                break;
        }

    }


    private static void choisirTypeDecompression(String type) throws IOException {
        switch (type) {
            case HUFFMAN:
                System.out.println("-huff -d " + pathFichierEntree + " " + pathFichierSortie);
                Huffman.getInstance().DecompressionHuffman(pathFichierEntree, pathFichierSortie);
                break;
            case LZW:
                System.out.println("-lzw -d " + pathFichierEntree + " " + pathFichierSortie);
                Lzw.getInstance().DecompressionLZW(pathFichierEntree, pathFichierSortie);
                break;
            case OPTIMISE:
                System.out.println("-opt -d " + pathFichierEntree + " " + pathFichierSortie);
                Optimise.getInstance().DecompressionOptimise(pathFichierEntree, pathFichierSortie);
                break;
        }
    }
}
