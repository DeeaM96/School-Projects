package tools;

import ressources.TypeUsine;

public class Aile extends Composante {
    public String type = TypeUsine.AILE;
    public String icone = "src/ressources/aile.png";

    public Aile() {
    }

    public String getIcone() {
        return icone;
    }

    public String getType() {
        return type;
    }
}
