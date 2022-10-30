package simulation;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import org.xml.sax.SAXException;
import ressources.TypeUsine;
import tools.XMLReader;

import javax.swing.*;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class Simulation {

    private static FenetrePrincipale fenetre;
    private static Environnement environnement;

    /**
     * Cette classe représente l'application dans son ensemble.
     */
    public static void main(String[] args) throws ParserConfigurationException, IOException, SAXException {
        environnement = new Environnement();
        fenetre = new FenetrePrincipale();
    }

    public static void SetSimulation(Entrepot entrepot, List<Usine> usines) {
        fenetre.SetSimulation(entrepot, usines);

        environnement.addPropertyChangeListener(fenetre);
        environnement.execute();
    }


}
