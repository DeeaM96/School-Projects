package tools;

import ressources.TypeUsine;

public class Moteur extends Composante {
    public String type = TypeUsine.MOTEUR;
    public String icone = "src/ressources/moteur.png";

    public Moteur() {
    }

    public String getIcone() {
        return icone;
    }

    public String getType() {
        return type;
    }
}
