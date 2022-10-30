package TP1;

import TP1.arbreHuffman.*;

import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import java.util.TreeMap;

import TP1.arbreHuffman.Noeud;

import java.util.*;

public class Huffman {

    private static Huffman instance;
    private static HashMap<Byte, String> codes;
    private int paddingBits;
    private int index;
    List<Byte> result = new ArrayList<Byte>();

    private Huffman() {
    }

    static Huffman getInstance() {
        if (instance == null) {
            instance = new Huffman();
        }

        return instance;
    }

    public void CompressionHuffman(String pathFichierEntree, String pathFichierSortie) throws IOException {

        codes = new HashMap<>();
        byte[] texte = Reader.getInstance().readFile(pathFichierEntree);


        if (texte != null) {

            TreeMap<Byte, Integer> tableFrequencesTrie = créerTableFréquences(texte);
            Arbre arbre = creerArbreHuffman(tableFrequencesTrie);
            HashMap<Byte, String> byteCodes = getBytes(arbre);

            byte[] texteCompresse = compresionTexte(texte, byteCodes);

            byte[] enTeteCompresse = compressionEnTete(tableFrequencesTrie);

            Writer.getInstance().ecrireFichierCompresse(pathFichierSortie, enTeteCompresse, texteCompresse);
        }
    }

    public void DecompressionHuffman(String pathFichierEntree, String pathFichierSortie) throws IOException {


        byte[] encodedFileContent = Reader.getInstance().readFile(pathFichierEntree);


        if (encodedFileContent != null) {
            TreeMap<Byte, Integer> tableFrequencesTrie = decompressionEnTete(encodedFileContent);

            Arbre arbre = creerArbreHuffman(tableFrequencesTrie);
            byte[] texte = decompressionTexte(encodedFileContent, arbre);

            if (texte != null) {
                Writer.getInstance().ecrireFichierDecompresse(pathFichierSortie, texte);
            }
        }
    }

    public Arbre creerArbreHuffman(TreeMap<Byte, Integer> tableFrequencesTrie) {
        ArrayList<Arbre> arbre = new ArrayList<>();

        for (Map.Entry<Byte, Integer> frequence : tableFrequencesTrie.entrySet()) {
            arbre.add(new Feuille(frequence.getKey(), frequence.getValue()));
        }

        while (arbre.size() > 1) {
            Noeud node = new Noeud(arbre.get(arbre.size() - 1), arbre.get(arbre.size() - 2));
            arbre.remove(arbre.size() - 1);
            arbre.remove(arbre.size() - 1);

            int i = 0;
            boolean indexTrouve = false;


            while (i != arbre.size() && !indexTrouve) {
                if (arbre.get(i).getFrequence() < node.getFrequence()) {
                    indexTrouve = true;
                } else {
                    ++i;
                }
            }

            arbre.add(i, node);
        }

        return arbre.get(0);
    }

    private void construireArbreHuffman(Arbre arbre, String code, int niveau) {
        if (arbre instanceof Noeud) {
            Noeud noeud = (Noeud) arbre;

            if (noeud.getGauche() != null) {
                construireArbreHuffman(noeud.getGauche(), code + "0", niveau + 1);
            }

            if (noeud.getDroit() != null) {
                construireArbreHuffman(noeud.getDroit(), code + "1", niveau + 1);
            }
        }
        if (arbre instanceof Feuille) {
            if (niveau == 1) {
                code = "0";
            }

            Feuille feuille = (Feuille) arbre;
            codes.put(feuille.getCharacter(), code);
        }
    }

    public HashMap<Byte, String> getBytes(Arbre arbre) {
        String code = "";

        construireArbreHuffman(arbre, code, 1);

        return codes;
    }

    private TreeMap<Byte, Integer> créerTableFréquences(byte[] bytes) {
        HashMap<Byte, Integer> tableFrequences = new HashMap<>();

        for (int i = 0; i != bytes.length; ++i) {
            if (tableFrequences.containsKey(bytes[i]))
                tableFrequences.put(bytes[i], tableFrequences.get(bytes[i]) + 1);
            else
                tableFrequences.put(bytes[i], 1);
        }

        MapValueComparator mapValueComparator = new MapValueComparator(tableFrequences);
        TreeMap<Byte, Integer> tableFrequencesTrie = new TreeMap<>(mapValueComparator);
        tableFrequencesTrie.putAll(tableFrequences);

        return tableFrequencesTrie;
    }

    private byte[] compresionTexte(byte[] bytes, HashMap<Byte, String> byteCodes) {
        String bitsTexteCompresse = "";
        StringBuilder bitsTexteCompresseSb = new StringBuilder();

        for (int i = 0; i != bytes.length; ++i) {
            bitsTexteCompresseSb.append(byteCodes.get(bytes[i]));
        }
        bitsTexteCompresse = bitsTexteCompresseSb.toString();

        ArrayList<String> stringBits = new ArrayList<>();
        StringBuilder bytesTexte = new StringBuilder();

        for (int i = 0; i != bitsTexteCompresse.length(); ++i) {
            bytesTexte.append(bitsTexteCompresse.charAt(i));

            if (bytesTexte.length() == 8 || i == bitsTexteCompresse.length() - 1) {
                stringBits.add(bytesTexte.toString());
                this.paddingBits = 8 - bytesTexte.length();
                bytesTexte.delete(0, bytesTexte.length());
            }
        }

        byte[] texteCompresse = new byte[stringBits.size()];

        for (int i = 0; i != stringBits.size(); ++i) {
            texteCompresse[i] = (byte) Integer.parseInt(stringBits.get(i), 2);
        }

        return texteCompresse;
    }

    public byte[] compressionEnTete(TreeMap<Byte, Integer> tableFrequencesTrie) {
        int[] headerInt = new int[256];
        Arrays.fill(headerInt, 0);

        try {
            for (Map.Entry<Byte, Integer> entry : tableFrequencesTrie.entrySet()) {
                headerInt[(entry.getKey() & 0xFF)] = entry.getValue();
            }

            String bitsChars = "";
            String padString = "00000000000000000000000000000000";
            StringBuilder bitesCharsSb = new StringBuilder();
            StringBuilder fraquencesCharacteresSb = new StringBuilder();
            String bitsFrequences = "";


            for (int i = 0; i != headerInt.length; ++i) {

                if (headerInt[i] == 0) {
                    bitesCharsSb.append('0');
                } else {
                    bitesCharsSb.append('1');
                    bitsFrequences = Integer.toBinaryString(headerInt[i]);
                    fraquencesCharacteresSb.append(padString.substring(bitsFrequences.length()));
                    fraquencesCharacteresSb.append(bitsFrequences);
                }

            }

            bitsChars = bitesCharsSb.append(fraquencesCharacteresSb.toString()).toString();

            ArrayList<String> stringBits = new ArrayList<>();
            StringBuilder sbEncodedTextBytes = new StringBuilder();

            for (int i = 0; i != bitsChars.length(); ++i) {

                sbEncodedTextBytes.append(bitsChars.charAt(i));

                if (sbEncodedTextBytes.length() == 8 || i == bitsChars.length() - 1) {
                    stringBits.add(sbEncodedTextBytes.toString());
                    sbEncodedTextBytes.delete(0, sbEncodedTextBytes.length());
                }

            }

            List<Byte> bytes = new ArrayList<Byte>();

            for (int i = 0; i != stringBits.size(); ++i) {

                bytes.add((byte) (Integer.parseInt(stringBits.get(i), 2) & 0xFF));

            }


            bytes.add(0, (byte) (this.paddingBits & 0xFF));

            return getArrayBytes(bytes);
        } catch (ArrayIndexOutOfBoundsException indexOutOfBound) {
            System.err.println(indexOutOfBound.getMessage());
        }

        return null;
    }

    public TreeMap<Byte, Integer> decompressionEnTete(byte[] fichierCompresse) {
        HashMap<Byte, Integer> tableFrequence = new HashMap<>();
        this.paddingBits = fichierCompresse[0] & 0xFF;

        StringBuilder enTeteCompresseSb = new StringBuilder();
        String padString = "00000000";
        String enTeteCompressePart = "";
        String enTeteCompresse = "";

        for (int i = 1; i != 33; ++i) {
            enTeteCompressePart = Integer.toBinaryString(fichierCompresse[i] & 0xFF);
            enTeteCompresseSb.append(padString.substring(enTeteCompressePart.length()));
            enTeteCompresseSb.append(enTeteCompressePart);
        }

        enTeteCompresse = enTeteCompresseSb.toString();

        int currentIndex = 33;
        int i = 0;

        while (i != enTeteCompresse.length()) {
            if (enTeteCompresse.charAt(i) == '1') {
                StringBuilder frequences = new StringBuilder();
                currentIndex = getIndex(fichierCompresse, padString, currentIndex, frequences);

                tableFrequence.put((byte) i, Integer.parseInt(frequences.toString(), 2));
            }
            i++;
        }

        this.index = currentIndex;

        MapValueComparator mapValueComparator = new MapValueComparator(tableFrequence);
        TreeMap<Byte, Integer> frequencesTries = new TreeMap<>(mapValueComparator);
        frequencesTries.putAll(tableFrequence);

        return frequencesTries;
    }

    public byte[] decompressionTexte(byte[] fichierCompresse, Arbre arbre) {
        StringBuilder texteCompresseSb = new StringBuilder();
        String padString = "00000000";
        String texteCompressePart = "";
        String texteCompresse = "";

        try {
            for (int i = this.index; i != fichierCompresse.length; ++i) {
                texteCompressePart = Integer.toBinaryString(fichierCompresse[i] & 0xFF);
                if (i != fichierCompresse.length - 1) {
                    texteCompresseSb.append(padString.substring(texteCompressePart.length()));
                } else {
                    texteCompresseSb.append(padString.substring(texteCompressePart.length() + this.paddingBits));
                }
                texteCompresseSb.append(texteCompressePart);
            }

            texteCompresse = texteCompresseSb.toString();


            if (arbre instanceof Noeud) {
                Noeud noeud = (Noeud) arbre;

                for (int i = 0; i != texteCompresse.length(); ++i) {
                    if (texteCompresse.charAt(i) == '0') {
                        if (noeud.getGauche() instanceof Noeud) {
                            noeud = (Noeud) noeud.getGauche();
                        } else if (noeud.getGauche() instanceof Feuille) {
                            result.add((((Feuille) noeud.getGauche()).getCharacter()));
                            noeud = (Noeud) arbre;
                        }
                    } else if (texteCompresse.charAt(i) == '1') {
                        if (noeud.getDroit() instanceof Noeud) {
                            noeud = (Noeud) noeud.getDroit();
                        } else if (noeud.getDroit() instanceof Feuille) {
                            result.add((((Feuille) noeud.getDroit()).getCharacter()));
                            noeud = (Noeud) arbre;
                        }
                    }

                }
            } else if (arbre instanceof Feuille) {
                Feuille feuille = (Feuille) arbre;

                for (int i = 0; i != texteCompresse.length(); ++i) {
                    result.add(feuille.getCharacter());
                }
            }

            return getArrayBytes(result);
        } catch (StringIndexOutOfBoundsException e) {
            System.out.println("The file has to be compressed first!");
        }

        return null;
    }


    private byte[] getArrayBytes(List<Byte> bytesList) {
        byte[] bytes = new byte[bytesList.size()];
        for (int i = 0; i < bytesList.size(); i++) {
            bytes[i] = bytesList.get(i);
        }

        return bytes;
    }

    private int getIndex(byte[] texteCompresse, String padString, int index, StringBuilder
            frequences) {
        getFrequence(texteCompresse, padString, index, index, frequences);
        return index + 4;
    }

    private void getFrequence(byte[] texteCompresse, String padString, int startIndex,
                              int index, StringBuilder frequences) {
        String frequence = Integer.toBinaryString(texteCompresse[index++] & 0xFF);
        frequences.append(padString.substring(frequence.length()));
        frequences.append(frequence);

        if (startIndex + 4 != index)
            getFrequence(texteCompresse, padString, startIndex, index, frequences);
    }


}
