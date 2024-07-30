public class Inventory {
    public string itemName;
    public int quantity;
    public string itemDescription;
    public Inventory() {
    }

    public Inventory(string itemName, int quantity, string itemDescription) {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemDescription = itemDescription;
    }
}
