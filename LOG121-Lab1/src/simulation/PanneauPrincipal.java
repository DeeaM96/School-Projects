package simulation;

import ressources.TypeUsine;
import tools.*;

import java.awt.Graphics;
import java.awt.Point;
import java.awt.image.BufferedImage;
import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.IOException;
import java.lang.reflect.Type;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.IntStream;

import javax.imageio.ImageIO;
import javax.swing.JPanel;

public class PanneauPrincipal extends JPanel {

    private static final long serialVersionUID = 1L;

    private long idComp = 0;

    private List<Composante> composantes = new ArrayList<Composante>() {
    };
    private List<Usine> usines = null;
    private Entrepot entrepot = null;

    public Boolean matiere = true;
    public Boolean assemblage = false;
    public Boolean aile = false;
    public Boolean moteur = false;
    public Boolean avion = false;

    private int matiereTimer;
    private int assemblageTimer;
    private int aileTimer;
    private int moteurTimer;
    private int entrepotTimer;

    private int compteurAvion;

    public PanneauPrincipal() {
    }

    public void SetSimulation(Entrepot entrepot, List<Usine> usines) {
        this.usines = usines;
        this.entrepot = entrepot;
    }

    @Override
    public void paint(Graphics g) {
        super.paint(g);

        if (usines != null && entrepot != null) {
            for (int i = 0; i < usines.size(); i++) {
                Object o = GetNextUsine(usines.get(i).chemin);
                if (o instanceof Usine)
                    g.drawLine(usines.get(i).x + (33 / 2), usines.get(i).y + (31 / 2), ((Usine) o).x + (33 / 2), ((Usine) o).y + (31 / 2));
                else if (o instanceof Entrepot)
                    g.drawLine(usines.get(i).x + (33 / 2), usines.get(i).y + (31 / 2), ((Entrepot) o).x + (33 / 2), ((Entrepot) o).y + (31 / 2));
            }

            List<Composante> tmpComp = new ArrayList<>();
            if (composantes != null) {
                for (Composante c : composantes) {
                    BufferedImage img = null;
                    try {
                        img = ImageIO.read(new FileInputStream(c.getIcone()));
                    } catch (IOException e) {
                        e.printStackTrace();
                    }

                    g.drawImage(img, c.position.x, c.position.y, img.getWidth(), img.getHeight(), null);
                    Point p = c.getVitesse(c.depart, c.arrive, c.position);
                    c.position.translate(p.x, p.y);

                    if (p.x == 0 && p.y == 0) {
                        tmpComp.add(c);

                        Object o = GetNextUsine(c.idArrive);
                        Usine u = null;
                        Entrepot e = null;
                        if (o instanceof Usine)
                            u = (Usine) o;
                        else if (o instanceof Entrepot)
                            e = (Entrepot) o;

                        if (e == null) {
                            u.ajouterEntree(c);

                            if (u.deuxEntree) {
                                if (u.e1 && u.e2) {
                                    if (u.type.equals(TypeUsine.ASSEMBLAGE))
                                        assemblage = true;
                                }
                            } else if (u.e1) {
                                switch (u.type) {
                                    case (TypeUsine.AILE):
                                        aile = true;
                                        break;
                                    case (TypeUsine.MOTEUR):
                                        moteur = true;
                                        break;
                                }
                            }

                        } else {
                            e.ajouterAvion();
                        }

                    }
                }

                for (Composante c : tmpComp) {
                    composantes.remove(c);
                }
            }

            for (Usine u : usines) {
                try {
                    ResetTimers(u);
                    BufferedImage img = GetImage(u);

                    if (img == null)
                        img = ImageIO.read(new FileInputStream(u.icones[0]));

                    g.drawImage(img, u.x, u.y, img.getWidth(), img.getHeight(), null);

                } catch (IOException e) {
                    e.printStackTrace();
                }
            }

            if (entrepotTimer > 3) {
                compteurAvion--;
                entrepot.qte--;
                entrepotTimer = 0;
            }

            if (entrepot.qte > 0)
                avion = true;
            else
                avion = false;

            try {
                BufferedImage img = ImageIO.read(new FileInputStream(entrepot.icones[entrepotTimer]));
                g.drawImage(img, entrepot.x, entrepot.y, img.getWidth(), img.getHeight(), null);
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }

    private BufferedImage GetImage(Usine u) {
        try {
            if (u.type.equals(TypeUsine.MATIERE)) {
                return ImageIO.read(new FileInputStream(u.icones[matiereTimer]));
            } else if (u.type.equals(TypeUsine.AILE)) {
                return ImageIO.read(new FileInputStream(u.icones[aileTimer]));
            } else if (u.type.equals(TypeUsine.MOTEUR)) {
                return ImageIO.read(new FileInputStream(u.icones[moteurTimer]));
            } else if (u.type.equals(TypeUsine.ASSEMBLAGE)) {
                return ImageIO.read(new FileInputStream(u.icones[assemblageTimer]));
            } else {
                return ImageIO.read(new FileInputStream(u.icones[0]));
            }
        } catch (IOException e) {
            e.printStackTrace();
            return null;
        } catch (ArrayIndexOutOfBoundsException array) {
            return null;
        }
    }

    private void ResetTimers(Usine u) {
        switch (u.type) {
            case (TypeUsine.MATIERE):
                if (matiereTimer > 3) {
                    SendNew(new Matiere(), u);
                    if (u.id == 12)
                        matiereTimer = 0;
                }
                break;
            case (TypeUsine.AILE):
                if (aileTimer > 3) {
                    SendNew(new Aile(), u);
                    aileTimer = 0;
                    aile = false;
                }
                break;
            case (TypeUsine.ASSEMBLAGE):
                if (assemblageTimer > 3) {
                    if (compteurAvion < entrepot.capacite) {
                        SendNew(new Assemblage(), u);
                        assemblageTimer = 0;
                        assemblage = false;
                        compteurAvion++;
                        matiere = true;
                    } else {
                        matiere = false;
                    }
                }
                break;
            case (TypeUsine.MOTEUR):
                if (moteurTimer > 3) {
                    SendNew(new Moteur(), u);
                    moteurTimer = 0;
                    moteur = false;
                }
                break;
        }
    }

    private void SendNew(Composante c, Usine u) {
        c.depart = new Point(u.x, u.y);
        c.position = new Point(u.x, u.y);

        Object o = GetNextUsine(u.chemin);
        if (o instanceof Usine)
            c.arrive = new Point(((Usine) o).x, ((Usine) o).y);
        else if (o instanceof Entrepot)
            c.arrive = new Point(((Entrepot) o).x, ((Entrepot) o).y);


        c.idArrive = u.chemin;
        c.id = idComp++;

        composantes.add(c);
    }

    private Object GetNextUsine(int chemin) {
        for (Usine u : usines)
            if (chemin == u.id)
                return u;
        return entrepot;
    }

    public Boolean isAvion() {
        return avion;
    }

    public Boolean isMatiere() {
        return matiere;
    }

    public Boolean isAssemblage() {
        return assemblage;
    }

    public Boolean isAile() {
        return aile;
    }

    public Boolean isMoteur() {
        return moteur;
    }

    public void setMatiereTimer() {
        matiereTimer++;
    }

    public void setMoteurTimer() {
        this.moteurTimer++;
    }

    public void setAssemblageTimer() {
        this.assemblageTimer++;
    }

    public void setAileTimer() {
        this.aileTimer++;
    }

    public void setEntrepotTimer() {
        this.entrepotTimer++;
    }
}