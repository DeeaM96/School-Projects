package TP1.arbreHuffman;

public class Noeud extends Arbre {
    private Arbre gauche, droit;


    public Noeud(Arbre gauche, Arbre droit) {
        super(gauche.getFrequence() + droit.getFrequence());
        this.gauche = gauche;
        this.droit = droit;
    }

    public Arbre getGauche() {
        return this.gauche;
    }

    public Arbre getDroit() {
        return this.droit;
    }
}
