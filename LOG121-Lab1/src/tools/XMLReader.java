package tools;

import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.DocumentBuilder;
import org.w3c.dom.Document;
import org.w3c.dom.NodeList;
import org.w3c.dom.Node;
import org.w3c.dom.Element;
import ressources.TypeUsine;

import java.io.File;

public class XMLReader {

    public static Document ReadConfig(String path){
        try {
            Document doc = DocumentBuilderFactory.newInstance().newDocumentBuilder().parse(new File("src/ressources/configuration.xml"));
            doc.getDocumentElement().normalize();

            System.out.println("Root element : " + doc.getDocumentElement().getNodeName());

            return doc;
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }

    public static String[][] GetEntree(NodeList u, int index) {
        if (index != TypeUsine.INDEX_MATIERE) {
            NodeList nl = ((Element) u.item(index)).getElementsByTagName("entree");
            if (nl.getLength() > 1)
                return new String[][]{{((Element)nl.item(0)).getAttribute("type"), ((Element)nl.item(0)).getAttribute("quantite")},{((Element)nl.item(1)).getAttribute("type"), ((Element)nl.item(1)).getAttribute("quantite")}};
            else if (nl.getLength() > 0)
                return new String[][]{{((Element)nl.item(0)).getAttribute("type"), ((Element)nl.item(0)).getAttribute("quantite")}};
        }
        return null;
    }

    public static String GetSortie(NodeList u, int index) {
        return ((Element)((Element) u.item(index))
                .getElementsByTagName("sortie").item(0)).getAttribute("type");
    }

    public static int GetIntervalProd(String type, NodeList u, int index) {
        int interval = Integer.parseInt(((Element) u.item(index))
                .getElementsByTagName("interval-production").item(0).getTextContent());
        SetDefaultInterval(type, interval);
        return interval;
    }

    private static void SetDefaultInterval(String type, int interval){
        switch  (type) {
            case TypeUsine.MATIERE:
                TypeUsine.INTERVALLE_MATIERE = interval;
                break;
            case TypeUsine.AILE:
                TypeUsine.INTERVALLE_AILE = interval;
                break;
            case TypeUsine.ASSEMBLAGE:
                TypeUsine.INTERVALLE_ASSEMBLAGE = interval;
                break;
            case TypeUsine.MOTEUR:
                TypeUsine.INTERVALLE_MOTEUR = interval;
                break;
        }
    }

    public static String[] GetIcones(NodeList u, int index) {
        String [] icones = new String[4];
        for (int i = 0; i < 4; i++)
            icones[i] = ((Element) ((Element) ((Element) u.item(index))
                    .getElementsByTagName("icones").item(0)).getElementsByTagName("icone")
                    .item(i)).getAttribute("path");

        return icones;
    }

    public static int GetIndexType(String type) {

        switch(type) {
            case TypeUsine.MATIERE:
                return TypeUsine.INDEX_MATIERE;
            case TypeUsine.AILE:
                return TypeUsine.INDEX_AILE;
            case TypeUsine.MOTEUR:
                return TypeUsine.INDEX_MOTEUR;
            case TypeUsine.ASSEMBLAGE:
                return TypeUsine.INDEX_ASSEMBLAGE;
            default:
                return TypeUsine.INDEX_ENTREPOT;
        }
    }
}
