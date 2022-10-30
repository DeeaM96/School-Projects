package tools;

import java.util.Random;

public class Strategie {
    public static String type = "Stratégie 1";

    public static long getIntervalle() {
        if (type.equals("Stratégie 1")) {
            return new Random().nextInt(4900) + 100;
        } else if (type.equals("Stratégie 2")) {
            return 200;
        } else
            return 200;
    }
}
