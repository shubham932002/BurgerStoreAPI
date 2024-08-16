

//document.addEventListener("DOMContentLoaded", function () {
//    updateCartTable();
//});

//async function updateCartTable() {
//    const userId = localStorage.getItem("userToken");
//    const response = await fetch(`https://localhost:7070/api/CartItems`);

//    // Check if the response is OK (status 200)
//    if (!response.ok) {
//        console.error('Failed to fetch cart items:', response.statusText);
//        return;
//    }

//    const cart = await response.json();

//    var cartTableBody = document.getElementById("cartTableBody");
//    var totalQuantity = document.getElementById("totalQuantity");
//    var totalPrice = document.getElementById("totalPrice");
//    var totalQty = 0;
//    var totalPriceValue = 0;
//    cartTableBody.innerHTML = "";

//    cart.forEach(function (item) {
//        var uniqueId = item.id;
//        var totalPriceItem = item.price * item.quantity;
//        totalQty += item.quantity;
//        totalPriceValue += totalPriceItem;


//        var row = document.createElement("tr");
//        row.innerHTML = `
//<td>${uniqueId}</td>
//<td>${item.burgerName}</td>
//<td>${item.category}</td>
//<td>Rs ${item.price}</td>
//<td>${item.quantity}</td>
//<td>Rs ${totalPriceItem}</td>
//<td><button class="btn-remove" onclick="removeFromCart(${item.id})">Remove</button></td>
//        `;
//        cartTableBody.appendChild(row);
//    });

//    totalQuantity.textContent = totalQty;
//    totalPrice.textContent = totalPriceValue.toFixed(2);
//}









//async function placeOrder() {
//    try {
//        const userId = localStorage.getItem("userToken");
//        const response = await fetch("https://localhost:7070/api/CartItems");
//        const cart = await response.json();
//        if (cart.length === 0) {
//            alert("Cart is empty!");
//            return;
//        }

//        // Loop through cart items and place each as a separate order
//        for (const item of cart) {
//            const order = {
//                burgerName: item.burgerName,
//                category: item.category,
//                price: item.price,
//                quantity: item.quantity,
//                totalPrice: item.totalPrice,
//                userId: userId
//            };

//            const orderResponse = await fetch("https://localhost:7070/api/orders", {
//                method: "POST",
//                headers: {
//                    "Content-Type": "application/json"
//                },
//                body: JSON.stringify(order)
//            });

//            if (!orderResponse.ok) {
//                console.error("Error placing the order:", orderResponse);
//                alert("Error placing the order. Please try again later.");
//                return;
//            }
//        }

//        alert("Order placed successfully!");
//        await fetch("https://localhost:7070/api/CartItems", { method: "DELETE" });
//        updateCartTable();
//    } catch (error) {
//        console.error("Error placing the order:", error);
//        alert("An error occurred while placing the order. Please try again later.");
//    }
//}


document.addEventListener("DOMContentLoaded", function () {
    updateCartTable();
});
async function updateCartTable() {
    const userId = localStorage.getItem("userToken");
    const response = await fetch(`https://localhost:7070/api/CartItems`);
    // Check if the response is OK (status 200)
    if (!response.ok) {
        console.error('Failed to fetch cart items:', response.statusText);
        return;
    }
    const cart = await response.json();
    var cartTableBody = document.getElementById("cartTableBody");
    var totalQuantity = document.getElementById("totalQuantity");
    var totalPrice = document.getElementById("totalPrice");
    var totalQty = 0;
    var totalPriceValue = 0;
    cartTableBody.innerHTML = "";
    cart.forEach(function (item) {
        var uniqueId = item.id;
        var totalPriceItem = item.price * item.quantity;
        totalQty += item.quantity;
        totalPriceValue += totalPriceItem;
        var row = document.createElement("tr");
        row.innerHTML = `
<td>${uniqueId}</td>
<td>${item.burgerName}</td>
<td>${item.category}</td>
<td>Rs ${item.price}</td>
<td>${item.quantity}</td>
<td>Rs ${totalPriceItem}</td>
<td><button class="btn-remove" onclick="removeFromCart(${item.id})">Remove</button></td>
        `;
        cartTableBody.appendChild(row);
    });
    totalQuantity.textContent = totalQty;
    totalPrice.textContent = totalPriceValue.toFixed(2);
}
// Function to remove an item from the cart
async function removeFromCart(itemId) {
    try {
        // Send a DELETE request to remove the item from the cart
        const response = await fetch(`https://localhost:7070/api/CartItems/${itemId}`, {
            method: "DELETE"
        });

        if (!response.ok) {
            console.error("Error removing item from cart:", response.statusText);
            alert("Error removing the item. Please try again later.");
            return;
        }

        // Update the cart table after removing the item
        updateCartTable();
    } catch (error) {
        console.error("Error removing item from cart:", error);
        alert("An error occurred while removing the item. Please try again later.");
    }
}
async function placeOrder() {
    try {
        const userId = localStorage.getItem("userToken");
        const response = await fetch("https://localhost:7070/api/CartItems");
        const cart = await response.json();
        if (cart.length === 0) {
            alert("Cart is empty!");
            return;
        }
        // Loop through cart items and place each as a separate order
        for (const item of cart) {
            const order = {
                burgerName: item.burgerName,
                category: item.category,
                price: item.price,
                quantity: item.quantity,
                totalPrice: item.totalPrice,
                userId: userId
            };
            const orderResponse = await fetch("https://localhost:7070/api/orders", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(order)
            });
            if (!orderResponse.ok) {
                console.error("Error placing the order:", orderResponse);
                alert("Error placing the order. Please try again later.");
                return;
            }
        }
        alert("Order placed successfully!");

        // Delete all items in the cart after placing the order
        await fetch("https://localhost:7070/api/CartItems", { method: "DELETE" });
        updateCartTable(); // Clear the cart on the front end as well
    } catch (error) {
        console.error("Error placing the order:", error);
        alert("An error occurred while placing the order. Please try again later.");
    }
}




