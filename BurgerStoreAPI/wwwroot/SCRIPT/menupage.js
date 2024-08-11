function addToCart(name, categoryId, quantityId) {
    var category = document.getElementById(categoryId).value;
    var quantity = document.getElementById(quantityId).value;
    var cart = JSON.parse(localStorage.getItem('cart')) || [];
    var price = category === 'veg' ? 100 : category === 'egg' ? 150 : 200;

    cart.push({ name, category, quantity, price });
    localStorage.setItem('cart', JSON.stringify(cart));
    alert(name + ' added to cart!');
}

function viewCart() {
    window.location.href = 'cart.html';
}