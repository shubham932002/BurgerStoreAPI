
//async function addToCart(name, categoryId, quantityId) {
//    const category = document.getElementById(categoryId).value;
//    const quantity = document.getElementById(quantityId).value;
//    const price = category === 'veg' ? 100 : category === 'egg' ? 150 : 200;

//    const cartItem = {
//        burgerName: name,
//        category: category,
//        price: price,
//        quantity: parseInt(quantity),
//        userId: localStorage.getItem("userToken") // Assuming user ID is stored in localStorage
//    };

//    const response = await fetch('https://localhost:7070/api/CartItems', {
//        method: 'POST',
//        headers: {
//            'Content-Type': 'application/json',
//        },
//        body: JSON.stringify(cartItem),
//    });

//    if (response.ok) {
//        alert(name + ' added to cart!');
//    } else {
//        alert('Failed to add item to cart');
//    }
//}

//async function fetchProducts() {
//    const response = await fetch('https://localhost:7070/api/Menus');
//    const products = await response.json();
//    renderBurgers(products);
//}

//function createBurgerCard(burger) {
//    return `
//        <div class="burger">
//            <h2><img src="${burger.imageUrl}" alt="${burger.name}"> ${burger.name}</h2>
//            <label for="category">Category:</label>
//            <select id="category">
//                <option value="veg">Veg - Rs 100</option>
//                <option value="egg">Egg - Rs 150</option>
//                <option value="chicken">Chicken - Rs 200</option>
//            </select>
//            <label for="quantity">Quantity:</label>
//            <input type="number" id="quantity" min="1" value="1">
//            <button class="btn" onclick="addToCart('${burger.name}', 'category', 'quantity')">Add to Cart</button>
//        </div>
//    `;
//}

//function renderBurgers(products) {
//    const container = document.getElementById('container');
//    products.forEach(burger => {
//        container.innerHTML += createBurgerCard(burger);
//    });
//}

//function viewCart() {
//    window.location.href = 'order.html';
//}

//fetchProducts();


async function addToCart(name, categoryId, quantityId) {
    const category = document.getElementById(categoryId).value;
    const quantity = document.getElementById(quantityId).value;
    const price = category === 'veg' ? 100 : category === 'egg' ? 150 : 200;
    const cartItem = {
        burgerName: name,
        category: category,
        price: price,
        quantity: parseInt(quantity),
        userId: localStorage.getItem("userToken") // Assuming user ID is stored in localStorage
    };
    const response = await fetch('https://localhost:7070/api/CartItems', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(cartItem),
    });
    if (response.ok) {
        alert(name + ' added to cart!');
    } else {
        alert('Failed to add item to cart');
    }
}

async function fetchProducts() {
    const response = await fetch('https://localhost:7070/api/Menus');
    const products = await response.json();
    renderBurgers(products);
}

function renderBurgers(products) {
    const container = document.getElementById('container');
    products.forEach((burger, index) => {
        container.innerHTML += createBurgerCard(burger, index);
    });
}

function createBurgerCard(burger, index) {
    return `
<div class="burger">
    <h2>
        <img src="${burger.imageUrl}" alt="${burger.name} image">
        ${burger.name}
    </h2>
    
    <label for="category-${index}">Category:</label>
    <select id="category-${index}">
        <option value="veg">Veg</option>
        <option value="egg">Egg</option>
        <option value="nonveg">Non-Veg</option>
    </select>
    
    <label for="quantity-${index}">Quantity:</label>
    <input type="number" id="quantity-${index}" value="1" min="1">
    
    <button class="btn" onclick="addToCart('${burger.name}', 'category-${index}', 'quantity-${index}')">Add to Cart</button>
</div>
    `;
}

function viewCart() {
    window.location.href = 'order.html';
}

fetchProducts();

