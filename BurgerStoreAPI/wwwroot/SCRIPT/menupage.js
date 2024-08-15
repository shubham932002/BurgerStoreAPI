function addToCart(name, categoryId, quantityId) {
    var category = document.getElementById(categoryId).value;
    var quantity = document.getElementById(quantityId).value;
    var cart = JSON.parse(localStorage.getItem('cart')) || [];
    var price = category === 'veg' ? 100 : category === 'egg' ? 150 : 200;
    


    cart.push({ name, category, quantity, price });
    localStorage.setItem('cart', JSON.stringify(cart));
    alert(name + ' added to cart!');
}

async function fetchProducts() {
    const response = await fetch('https://localhost:7070/api/Menus');
    const products = await response.json();
    renderBurgers(products);
}

function createBurgerCard(burger) {
    return `
        <div class="burger">
            <h2><img src="${burger.imageUrl}" alt="${burger.name}"> ${burger.name}</h2>
            <label for="category">Category:</label>
            <select id="category">
                <option value="veg">Veg - Rs 100</option>
                <option value="egg">Egg - Rs 150</option>
                <option value="chicken">Chicken - Rs 200</option>
            </select>
            <label for="quantity">Quantity:</label>
            <input type="number" id="quantity" min="1" value="1">
            <button class="btn" onclick="addToCart('${burger.name}', 'category', 'quantity')">Add to Cart</button>
        </div>
    `;
}

function renderBurgers(products) {
    const container = document.getElementById('container');
    products.forEach(burger => {
        container.innerHTML += createBurgerCard(burger);
    });
}

function viewCart() {
    window.location.href = 'order.html';
}

fetchProducts();