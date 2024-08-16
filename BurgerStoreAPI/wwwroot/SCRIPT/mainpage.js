
const API_URLS = {
    USERS: 'https://localhost:7070/api/users',
    ADMINS: 'https://localhost:7070/api/admins',
    AUTH: 'https://localhost:7070/api/auth/login'
};
const MOBILE_NUMBER_REGEX = /^\d{10}$/;

// Utility functions
function showAlert(message) {
    alert(message);
}

async function fetchData(url) {
    try {
        const response = await fetch(url);
        if (!response.ok) throw new Error(`Failed to fetch data from ${url}`);
        return await response.json();
    } catch (error) {
        console.error('Error fetching data:', error);
    
        return [];
    }
}

function validateInputs(name, mobileNumber, isAdmin = false) {
    if (!name || !mobileNumber) {
        showAlert("Both name and mobile number must be entered.");
        return false;
    }
    if (!isAdmin && !MOBILE_NUMBER_REGEX.test(mobileNumber)) {
        showAlert("Mobile number must be 10 digits and in number format.");
        return false;
    }
    return true;
}

// User-related functions
async function validateUser(event) {
    event.preventDefault();

    const name = document.getElementById('nameInput').value.trim();
    const mobileNumber = document.getElementById('phoneInput').value.trim();

    if (!validateInputs(name, mobileNumber)) return;

    try {
        const response = await fetch(API_URLS.AUTH, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ name, mobileNumber })
        });

        if (response.ok) {
            const data = await response.json();
            localStorage.setItem("userToken", data.token);
            showAlert('Welcome ' + name + '!');
            window.location.href = 'menupage.html';
        } else {
            showAlert('Access denied. Invalid user credentials.');
        }
    } catch (error) {
        console.error('Error during login:', error);
        showAlert('An error occurred while logging in.');
    }
}

async function signupUser(event) {
    event.preventDefault();

    const name = document.getElementById('signupNameInput').value.trim();
    const mobileNumber = document.getElementById('signupPhoneInput').value.trim();

    if (!validateInputs(name, mobileNumber)) return;

    const users = await fetchData(API_URLS.USERS);
    const existingUser = users.find(user => user.mobileNumber === mobileNumber);

    if (existingUser) {
        showAlert("Mobile number is already registered.");
        return;
    }

    try {
        const signupResponse = await fetch(API_URLS.USERS, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ name, mobileNumber })
        });

        if (signupResponse.ok) {
            showAlert('Signup successful! You can now log in.');
            document.getElementById('signupForm').reset();
        } else {
            showAlert('Signup failed. Please try again.');
        }
    } catch (error) {
        console.error('Error during signup:', error);
       
    }
}

// Admin-related functions
async function validateAdmin(event) {
    event.preventDefault();

    const userName = document.getElementById('adminNameInput').value.trim();
    const password = document.getElementById('adminPasswordInput').value.trim();

    if (!validateInputs(userName, password, true)) return;

    const admins = await fetchData(API_URLS.ADMINS);
    const existingAdmin = admins.find(admin => admin.userName === userName && admin.password === password);

    if (existingAdmin) {
        localStorage.setItem("adminToken", existingAdmin.id); // Use Id instead of adminId
        showAlert('Admin login successful!');
        window.location.href = 'admin.html';
    } else {
        showAlert('Access denied. Invalid admin credentials.');
    }
}

// Toggle between user and admin forms
document.getElementById('userToggle').addEventListener('click', () => {
    document.getElementById('userFormContainer').style.display = 'block';
    document.getElementById('adminFormContainer').style.display = 'none';
    document.getElementById('userToggle').classList.add('active');
    document.getElementById('adminToggle').classList.remove('active');
});
document.getElementById('adminToggle').addEventListener('click', () => {
    document.getElementById('userFormContainer').style.display = 'none';
    document.getElementById('adminFormContainer').style.display = 'block';
    document.getElementById('adminToggle').classList.add('active');
    document.getElementById('userToggle').classList.remove('active');
})

// Event listeners
document.getElementById('loginButton').addEventListener('click', validateUser);
document.getElementById('signupButton').addEventListener('click', signupUser);
document.getElementById('adminLoginButton').addEventListener('click', validateAdmin);


