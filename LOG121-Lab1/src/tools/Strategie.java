package tools;

import java.util.Random;

public class Strategie {
    public static String type = "Strat�gie 1";

    public static long getIntervalle() {
        if (type.equals("Strat�gie 1")) {
            return new Random().nextInt(4900) + 100;
        } else if (type.equals("Strat�gie 2")) {
            return 200;
        } else
            return 200;
    }
}
