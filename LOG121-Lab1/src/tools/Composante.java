package tools;

import java.awt.*;

public abstract class Composante implements Cloneable {
    public long id = 0;
    public int idArrive;
    public Point depart = new Point();
    public Point arrive = new Point();
    public Point position = new Point();

    public Composante() {
    }

    public Object clone() throws
            CloneNotSupportedException {
        return super.clone();
    }

    public Point getVitesse(Point depart, Point arrive, Point position) {
        int x1 = arrive.x;
        int x2 = position.x;
        int y1 = arrive.y;
        int y2 = position.y;
        int x = x2 - x1;
        int y = y2 - y1;

        if (x == 0 && y == 0)
            return new Point(0, 0);

        if (x == 0) {
            x = 0;
        } else if (x <= 1) {
            x = findValue(x);
        } else {
            x = -findValue(x);
        }

        if (y == 0) {
            y = 0;
        } else if (y < 1) {
            y = findValue(y);
        } else {
            y = -findValue(y);
        }

        return new Point(x, y);
    }

    public int findValue(int x) {
        for (int i = 10; i < 20; i++)
            if (Math.abs(x) % i == 0)
                return i;

        return 1;
    }

    public abstract String getIcone();

    public abstract String getType();
}
