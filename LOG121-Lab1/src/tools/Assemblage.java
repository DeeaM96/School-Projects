package tools;

import ressources.TypeUsine;

public class Assemblage extends Composante {
    public String type = TypeUsine.TYPE_AVION;
    public String icone = "src/ressources/avion.png";

    public Assemblage() {
    }

    public String getIcone() {
        return icone;
    }

    public String getType() {
        return type;
    }
}
