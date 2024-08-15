document.addEventListener("DOMContentLoaded", function () {
    updateCartTable();
});
function updateCartTable() {
    var cart = JSON.parse(localStorage.getItem("cart")) || [];
    var cartTableBody = document.getElementById("cartTableBody");
    var totalQuantity = document.getElementById("totalQuantity");
    var totalPrice = document.getElementById("totalPrice");
    var totalQty = 0;
    var totalPriceValue = 0;
    cartTableBody.innerHTML = "";
    cart.forEach(function (item, index) {
        var uniqueId = index + 1;
        var totalPriceItem = item.price * item.quantity;
        totalQty += parseInt(item.quantity);
        totalPriceValue += totalPriceItem;
        var row = document.createElement("tr");
        row.innerHTML = `
<td>${uniqueId}</td>
<td>${item.name}</td>
<td>${item.category}</td>
<td>Rs ${item.price}</td>
<td>${item.quantity}</td>
<td>Rs ${totalPriceItem}</td>
<td><button class="btn-remove" onclick="removeFromCart(${index})">Remove</button></td>
                `;
        cartTableBody.appendChild(row);
    });
    totalQuantity.textContent = totalQty;
    totalPrice.textContent = totalPriceValue.toFixed(2);
}
function removeFromCart(index) {
    var cart = JSON.parse(localStorage.getItem("cart")) || [];
    cart.splice(index, 1);
    localStorage.setItem("cart", JSON.stringify(cart));
    updateCartTable();
}

async function placeOrder() {
    try {
        // Get the cart data from localStorage
        const cart = JSON.parse(localStorage.getItem("cart")) || [];

        // Create the order object
        const order = {
            burgerName: "",
            category: "",
            price: 0,
            quantity: 0,
            totalPrice: 0,
            userId: 1, // Assuming user ID 1 for now
        };

        // Populate the order object with data from the cart
        cart.forEach((item) => {
            order.burgerName = item.name;
            order.category = item.category;
            order.price = item.price;
            order.quantity = item.quantity;
            order.totalPrice = item.price * item.quantity;
            order.userId = localStorage.getItem("userToken");
        });

        console.log("Sending POST request to /api/orders");
        // Send the POST request to place the order
        const response = await fetch("https://localhost:7070/api/orders", {

            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(order),
        });
        console.log("Response status:", response.status);
        if (response.ok) {
            // Order placed successfully
            alert("Order placed successfully!");
            localStorage.removeItem("cart"); // Clear the cart after successful order
            updateCartTable(); // Update the cart table
        } else {
            // Error placing the order
            alert("Error placing the order. Please try again later.");
        }
    } catch (error) {
        console.error("Error placing the order:", error);
        alert("An error occurred while placing the order. Please try again later.");
    }
}