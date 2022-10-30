package simulation;

import ressources.TypeUsine;
import tools.Composante;

import java.util.Observable;
import java.util.Observer;

public class Usine implements Observer {
    public String type;
    public int id;
    public int x, y;
    public int chemin;
    public String[] icones;
    public int intervalProd;
    public int intervalFabri;
    public String sortie;
    public String[][] entree;

    public Boolean deuxEntree = false;
    public Boolean e1 = false;
    public Boolean e2 = false;
    public int entree1 = 0;
    public int entree2 = 0;

    public Usine(String type, int id, int x, int y, String[] icones, int intervalProd, String sortie, String[][] entree) {
        this.type = type;
        this.id = id;
        this.x = x;
        this.y = y;
        this.icones = icones;
        this.intervalProd = intervalProd;
        this.intervalFabri = intervalProd * 4;
        this.sortie = sortie;
        this.entree = entree;

        if (entree != null && entree.length >= 2)
            deuxEntree = true;
    }

    @Override
    public void update(Observable o, Object arg) {

    }

    public void ajouterEntree(Composante c) {
        if (entree.length >= 2) {
            if (entree[0][0].equals(TypeUsine.TYPE_AILE) || entree[0][0].equals(TypeUsine.TYPE_MOTEUR))
                if (entree1 < Integer.parseInt(entree[0][1]))
                    entree1++;
                else
                    e1 = true;
            if (entree[1][0].equals(TypeUsine.TYPE_MOTEUR) || entree[1][0].equals(TypeUsine.TYPE_AILE))
                if (entree2 < Integer.parseInt(entree[1][1]))
                    entree2++;
                else
                    e2 = true;
        } else if (entree.length >= 1)
            if (entree[0][0].equals(TypeUsine.TYPE_MATIERE))
                if (entree1 < Integer.parseInt(entree[0][1]))
                    entree1++;
                else
                    e1 = true;
    }
}
