package tools;

import ressources.TypeUsine;

public class Matiere extends Composante {
    public String type = TypeUsine.MATIERE;
    public String icone = "src/ressources/metal.png";

    public Matiere() {
    }

    public String getIcone() {
        return icone;
    }

    public String getType() {
        return type;
    }
}
