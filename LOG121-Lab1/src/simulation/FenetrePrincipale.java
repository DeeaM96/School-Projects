package simulation;

import ressources.TypeUsine;

import java.awt.*;
import java.beans.PropertyChangeEvent;
import java.beans.PropertyChangeListener;
import java.util.List;

import javax.swing.JFrame;

public class FenetrePrincipale extends JFrame implements PropertyChangeListener {

    private static final long serialVersionUID = 1L;
    private static final String TITRE_FENETRE = "Laboratoire 1 : LOG121 - Simulation";
    private static final Dimension DIMENSION = new Dimension(700, 700);

    private PanneauPrincipal panneauPrincipal;

    public FenetrePrincipale() {
        MenuFenetre menuFenetre = new MenuFenetre();

        panneauPrincipal = new PanneauPrincipal();
        add(panneauPrincipal);

        add(menuFenetre, BorderLayout.NORTH);
        // Faire en sorte que le X de la fenêtre ferme la fenêtre
        setDefaultCloseOperation(EXIT_ON_CLOSE);
        setTitle(TITRE_FENETRE);
        setSize(DIMENSION);
        // Rendre la fenêtre visible
        setVisible(true);
        // Mettre la fenêtre au centre de l'écran
        setLocationRelativeTo(null);
        // Empêcher la redimension de la fenêtre
        setResizable(false);
    }

    public void SetSimulation(Entrepot entrepot, List<Usine> usines) {
        panneauPrincipal.SetSimulation(entrepot, usines);
    }

    @Override
    public void propertyChange(PropertyChangeEvent evt) {
        repaint();
        if (evt.getPropertyName().equals("AVION")) {
            //if entrepot = plein then stop
            if (panneauPrincipal.isAvion())
                panneauPrincipal.setEntrepotTimer();
        }
        if (evt.getPropertyName().equals("MATIERE")) {
            //if entrepot = plein then stop
            if (panneauPrincipal.isMatiere())
                panneauPrincipal.setMatiereTimer();
        }
        if (evt.getPropertyName().equals("MOTEUR")) {
            //if entrepot = plein then stop
            //if 4 matiere then go. On full then send and stop.
            if (panneauPrincipal.isMoteur())
                panneauPrincipal.setMoteurTimer();
        }
        if (evt.getPropertyName().equals("ASSEMBLAGE")) {
            //if entrepot = plein then stop
            //if 2 aile, 1 moteur then go. On full then send and stop.
            if (panneauPrincipal.isAssemblage())
                panneauPrincipal.setAssemblageTimer();
        }
        if (evt.getPropertyName().equals("AILE")) {
            //if entrepot = plein then stop
            //if 2 matiere then go. On full then send and stop.
            if (panneauPrincipal.isAile())
                panneauPrincipal.setAileTimer();
        }
    }
}
