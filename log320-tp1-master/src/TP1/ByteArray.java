package TP1;

import java.util.Arrays;

public class ByteArray {
    private byte[] array;

    public ByteArray(byte[] array) {
        this.array = array;
    }

    public byte[] getArray() {
        return this.array;
    }

    public int length() {
        return array.length;
    }

    @Override
    public int hashCode() {
        return Arrays.hashCode(array);
    }

    @Override
    public boolean equals(Object obj) {
        return Arrays.equals(array, ((ByteArray) obj).getArray());
    }

    @Override
    public String toString() {
        return new String(array);
    }
}
