

document.addEventListener('DOMContentLoaded', fetchBurgers);

const burgerForm = document.getElementById('burgerForm');
burgerForm.addEventListener('submit', (e) => {
    e.preventDefault();
    const id = document.getElementById('burgerId').value;
    if (id) {
        updateBurger(id);
    } else {
        addBurger();
    }
});

async function fetchBurgers() {
    const response = await fetch('https://localhost:7070/api/Menus');
    const burgers = await response.json();
    renderBurgers(burgers);
}

function renderBurgers(burgers) {
    const burgerList = document.getElementById('burgerList');
    burgerList.innerHTML = ''; // Clear existing list
    burgers.forEach(burger => {
        burgerList.innerHTML += createBurgerCard(burger);
    });
}

function createBurgerCard(burger) {
    return `
        <div class="burger" data-id="${burger.id}">
            <h2><img src="${burger.imageUrl}" alt="${burger.name}"> ${burger.name}</h2>
            <p>Category: ${burger.category}</p>
            <p>Price: Rs ${burger.price}</p>
            <button onclick="editBurger(${burger.id})">Edit</button>
            <button onclick="deleteBurger(${burger.id})">Delete</button>
        </div>
    `;
}

function addBurger() {
    const name = document.getElementById('burgerName').value;
    const category = document.getElementById('category').value;
    const price = document.getElementById('price').value;
    const imageUrl = document.getElementById('imageUrl').value;

    fetch('https://localhost:7070/api/Menus', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ name, category, price, imageUrl })
    })
        .then(response => response.json())
        .then(() => {
            fetchBurgers();
            burgerForm.reset();
        });
}

function editBurger(id) {
    const burgerCard = document.querySelector(`.burger[data-id="${id}"]`);
    const name = burgerCard.querySelector('h2').innerText;
    const category = burgerCard.querySelector('p').innerText.split(': ')[1].toLowerCase();
    const price = burgerCard.querySelector('p:nth-child(3)').innerText.split(': ')[1];
    const imageUrl = burgerCard.querySelector('img').src;

    document.getElementById('burgerName').value = name;
    document.getElementById('category').value = category;
    document.getElementById('price').value = price;
    document.getElementById('imageUrl').value = imageUrl;
    document.getElementById('burgerId').value = id;
}

function updateBurger(id) {
    const name = document.getElementById('burgerName').value;
    const category = document.getElementById('category').value;
    const price = document.getElementById('price').value;
    const imageUrl = document.getElementById('imageUrl').value;

    fetch(`https://localhost:7070/api/Menus/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ name, category, price, imageUrl })
    })
        .then(response => response.json())
        .then(() => {
            fetchBurgers();
            burgerForm.reset();
        });
}

function deleteBurger(id) {
    fetch(`https://localhost:7070/api/Menus/${id}`, {
        method: 'DELETE'
    })
        .then(() => {
            fetchBurgers();
        });
}