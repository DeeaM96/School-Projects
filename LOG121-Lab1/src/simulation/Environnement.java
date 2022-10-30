package simulation;

import ressources.TypeUsine;
import tools.Strategie;

import javax.swing.SwingWorker;

public class Environnement extends SwingWorker<Object, String> {
    private boolean actif = true;
    private static final int DELAI_DEPLACEMENT = 100;

    @Override
    protected Object doInBackground() throws Exception {
        firePropertyChange("DESSIN", null, "Dessin de base");

        new Thread(() -> {
            while (actif) {
                try {
                    Thread.sleep(DELAI_DEPLACEMENT);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
                firePropertyChange("DEPLACEMENT", null, "Deplacement de la production");
            }
        }).start();

        new Thread(() -> {
            while (actif) {
                try {
                    Thread.sleep(Strategie.getIntervalle());
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
                firePropertyChange("AVION", null, "Interval matiere");
            }
        }).start();

        new Thread(() -> {
            while (actif) {
                try {
                    Thread.sleep(TypeUsine.INTERVALLE_MATIERE);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
                firePropertyChange("MATIERE", null, "Interval matiere");
            }
        }).start();

        new Thread(() -> {
            while (actif) {
                try {
                    Thread.sleep(TypeUsine.INTERVALLE_ASSEMBLAGE);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
                firePropertyChange("ASSEMBLAGE", null, "Interval assemblage");
            }
        }).start();

        new Thread(() -> {
            while (actif) {
                try {
                    Thread.sleep(TypeUsine.INTERVALLE_AILE);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
                firePropertyChange("AILE", null, "Interval aile");
            }
        }).start();

        new Thread(() -> {
            while (actif) {
                try {
                    Thread.sleep(TypeUsine.INTERVALLE_MOTEUR);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
                firePropertyChange("MOTEUR", null, "Interval moteur");
            }
        }).start();
        return null;
    }

}