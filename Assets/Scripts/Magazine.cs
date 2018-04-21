
public class Magazine {

    static int PISTOL_MAX = 15;
    static int SHOTGUN_MAX = 8;
    static int RIFLE_MAX = 35;
    static int BAZOOKA_MAX = 1;

    public int maxbullets;
    public int currentbullets;


    public static Magazine PistolMagazine(){
    	return new Magazine(PISTOL_MAX);
    }

    public static Magazine RifleMagazine(){
    	return new Magazine(RIFLE_MAX);
    }

    public static Magazine ShotgunMagazine(){
    	return new Magazine(SHOTGUN_MAX);
    }

    public static Magazine BazookaMagazine(){
    	return new Magazine(BAZOOKA_MAX);
    }

    //The magazine is created full
    public Magazine(int capacity) {
    	maxbullets = capacity;
    	currentbullets = capacity;
    }

    //Remove one bullet
    public void RemoveBullet(){
    	currentbullets--;
    }

    //Return true if the magazine has no bullets
    public bool IsEmpty(){

    	if(currentbullets == 0){
    		return true;
    	}else{
    		return false;
    	}
    }
}