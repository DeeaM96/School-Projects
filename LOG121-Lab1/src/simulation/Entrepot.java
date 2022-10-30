package simulation;

import java.util.Observable;
import java.util.Observer;

public class Entrepot implements Observer {
    public int id;
    public int x, y;
    public String[] icones;
    public String entree;
    public int capacite;
    public int qte;

    public Entrepot() {

    }

    public Entrepot(int id, int x, int y, String[] icones, String entree, String capacite) {
        this.id = id;
        this.x = x;
        this.y = y;
        this.icones = icones;
        this.entree = entree;
        this.capacite = Integer.parseInt(capacite);
    }

    public void ajouterAvion() {
        qte++;
    }

    @Override
    public void update(Observable o, Object arg) {

    }
}
