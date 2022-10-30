package simulation;

import org.w3c.dom.Element;
import org.w3c.dom.NodeList;
import ressources.TypeUsine;
import tools.XMLReader;
import org.w3c.dom.Document;

import java.awt.event.ActionEvent;
import java.io.File;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import javax.swing.JFileChooser;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;
import javax.swing.JOptionPane;
import javax.swing.filechooser.FileNameExtensionFilter;
import javax.swing.filechooser.FileSystemView;

public class MenuFenetre extends JMenuBar {

    private static final long serialVersionUID = 1L;
    private static final String MENU_FICHIER_TITRE = "Fichier";
    private static final String MENU_FICHIER_CHARGER = "Charger";
    private static final String MENU_FICHIER_QUITTER = "Quitter";
    private static final String MENU_SIMULATION_TITRE = "Simulation";
    private static final String MENU_SIMULATION_CHOISIR = "Choisir";
    private static final String MENU_AIDE_TITRE = "Aide";
    private static final String MENU_AIDE_PROPOS = "À propos de...";

    private Entrepot entrepot = new Entrepot();
    private List<Usine> usines = new ArrayList<Usine>();

    public MenuFenetre() {
        ajouterMenuFichier();
        ajouterMenuSimulation();
        ajouterMenuAide();
    }

    /**
     * Créer le menu de Fichier
     */
    private void ajouterMenuFichier() {
        JMenu menuFichier = new JMenu(MENU_FICHIER_TITRE);
        JMenuItem menuCharger = new JMenuItem(MENU_FICHIER_CHARGER);
        JMenuItem menuQuitter = new JMenuItem(MENU_FICHIER_QUITTER);

        menuCharger.addActionListener((ActionEvent e) -> {
            JFileChooser fileChooser = new JFileChooser(FileSystemView.getFileSystemView().getHomeDirectory());
            fileChooser.setDialogTitle("Sélectionnez un fichier de configuration");
            fileChooser.setAcceptAllFileFilterUsed(false);
            // Créer un filtre
            FileNameExtensionFilter filtre = new FileNameExtensionFilter(".xml", "xml");
            fileChooser.addChoosableFileFilter(filtre);

            int returnValue = fileChooser.showOpenDialog(null);

            if (returnValue == JFileChooser.APPROVE_OPTION) {
                Document doc = XMLReader.ReadConfig(fileChooser.getSelectedFile().getAbsolutePath());

                if (doc == null)
                    JOptionPane.showMessageDialog(null, "The configuration file could not be read.", "Warning!", JOptionPane.WARNING_MESSAGE);
                else {
                    GenerateObjects(doc);
                    GenerateChemins(doc);
                    Simulation.SetSimulation(entrepot, usines);
                }
            }
        });

        menuQuitter.addActionListener((ActionEvent e) -> {
            System.exit(0);
        });

        menuFichier.add(menuCharger);
        menuFichier.add(menuQuitter);

        add(menuFichier);

    }

    private void GenerateChemins(Document doc) {
        List<List<Integer>> chemins = new ArrayList<>();
        NodeList c = doc.getElementsByTagName("chemin");
        for (int i = 0; i < c.getLength(); i++) {
            chemins.add(new ArrayList<Integer>(Arrays.asList(Integer.parseInt(((Element) c.item(i)).getAttribute("de")), Integer.parseInt(((Element) c.item(i)).getAttribute("vers")))));
        }

        UpdateChemins(chemins);
    }

    private void UpdateChemins(List<List<Integer>> chemins) {
        for (int i = 0; i < usines.size(); i++) {
            for (int j = 0; j < chemins.size(); j++)
                if (usines.get(i).id == chemins.get(j).get(0))
                    usines.get(i).chemin = chemins.get(j).get(1);
        }
    }

    private void GenerateObjects(Document doc) {
        NodeList u = doc.getElementsByTagName("usine");

        for (int i = 5; i < u.getLength(); i++) {
            String type = ((Element) u.item(i)).getAttribute("type");
            int id = Integer.parseInt(((Element) u.item(i)).getAttribute("id"));
            int x = Integer.parseInt(((Element) u.item(i)).getAttribute("x"));
            int y = Integer.parseInt(((Element) u.item(i)).getAttribute("y"));

            int index = XMLReader.GetIndexType(type);
            if (type.equals(TypeUsine.ENTREPOT))
                entrepot = new Entrepot(id, x, y, XMLReader.GetIcones(u, index), ((Element) ((Element) u.item(index))
                        .getElementsByTagName("entree").item(0)).getAttribute("type"), ((Element) ((Element) u.item(index))
                        .getElementsByTagName("entree").item(0)).getAttribute("capacite"));
            else
                usines.add(new Usine(type, id, x, y, XMLReader.GetIcones(u, index), XMLReader.GetIntervalProd(type, u, index), XMLReader.GetSortie(u, index), XMLReader.GetEntree(u, index)));
        }
    }

    /**
     * Créer le menu de Simulation
     */
    private void ajouterMenuSimulation() {
        JMenu menuSimulation = new JMenu(MENU_SIMULATION_TITRE);
        JMenuItem menuChoisir = new JMenuItem(MENU_SIMULATION_CHOISIR);
        menuSimulation.add(menuChoisir);

        menuChoisir.addActionListener((ActionEvent e) -> {
            // Ouvrir la fenêtre de sélection
            // TODO - Récupérer la bonne stratégie de vente
            new FenetreStrategie();
        });
        add(menuSimulation);

    }

    /**
     * Créer le menu Aide
     */
    private void ajouterMenuAide() {
        JMenu menuAide = new JMenu(MENU_AIDE_TITRE);
        JMenuItem menuPropos = new JMenuItem(MENU_AIDE_PROPOS);
        menuAide.add(menuPropos);

        menuPropos.addActionListener((ActionEvent e) -> {
            JOptionPane.showMessageDialog(null,
                    "<html><p>Application simulant une chaine de production d'avions.</p>" + "<br>"
                            + "<p>&copy; &nbsp; 2017 &nbsp; Ghizlane El Boussaidi</p>"
                            + "<p>&copy; &nbsp; 2017 &nbsp; Dany Boisvert</p>"
                            + "<p>&copy; &nbsp; 2017 &nbsp; Vincent Mattard</p>" + "<br>"
                            + "<p>&Eacute;cole de technologie sup&eacute;rieure</p></html>");
        });
        add(menuAide);
    }

}
