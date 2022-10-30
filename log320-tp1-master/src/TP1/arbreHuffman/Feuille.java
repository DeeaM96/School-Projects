package TP1.arbreHuffman;

public class Feuille extends Arbre {

    private byte character;

    public Feuille(byte character, int frequency) {
        super(frequency);
        this.character = character;
    }

    public byte getCharacter() {
        return this.character;
    }
}
