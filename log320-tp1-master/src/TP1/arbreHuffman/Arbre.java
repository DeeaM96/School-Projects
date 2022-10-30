package TP1.arbreHuffman;

import java.io.Serializable;

public abstract class Arbre implements Comparable<Arbre>, Serializable {
    private int frequency;

    Arbre(int frequency) {
        this.frequency = frequency;
    }

    public int compareTo(Arbre arbre) {
        return this.getFrequence() - arbre.getFrequence();
    }

    public int getFrequence() {
        return this.frequency;
    }
}
