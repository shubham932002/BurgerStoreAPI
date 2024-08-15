

const USER_API_URL = 'https://localhost:7070/api/users';
const ADMIN_API_URL = 'https://localhost:7070/api/admins';  // Assuming a separate endpoint for admin

const MOBILE_NUMBER_REGEX = /^\d{10}$/;

function showAlert(message) {
    alert(message);
}

function validateInputs(name, mobileNumber) {
    if (!name || !mobileNumber) {
        showAlert("Both name and mobile number must be entered.");
        return false;
    }
    if (!MOBILE_NUMBER_REGEX.test(mobileNumber)) {
        showAlert("Mobile number must be 10 digits and in number format.");
        return false;
    }
    return true;
}

// Fetch users from the API
async function fetchUsers() {
    try {
        const response = await fetch(USER_API_URL);
        if (!response.ok) throw new Error('Failed to fetch users');
        return await response.json();
    } catch (error) {
        console.error('Error fetching users:', error);
        showAlert('An error occurred while fetching users.');
        return [];
    }
}

// Fetch admins from the API
async function fetchAdmins() {
    try {
        const response = await fetch(ADMIN_API_URL);
        if (!response.ok) throw new Error('Failed to fetch admins');
        return await response.json();
    } catch (error) {
        console.error('Error fetching admins:', error);
        showAlert('An error occurred while fetching admins.');
        return [];
    }
}

// Validate user login
async function validateUser(event) {
    event.preventDefault();

    const name = document.getElementById('nameInput').value.trim();
    const mobileNumber = document.getElementById('phoneInput').value.trim();

    if (!validateInputs(name, mobileNumber)) return;

    const users = await fetchUsers();
    const existingUser = users.find(user => user.name === name && user.mobileNumber === mobileNumber);

    if (existingUser) {
        localStorage.setItem("userToken", existingUser.userId);
        showAlert('Welcome ' + name + '!');
        window.location.href = 'menupage.html';
    } else {
        showAlert('Access denied. Invalid user credentials.');
    }
}

// Signup new user
async function signupUser(event) {
    event.preventDefault();

    const name = document.getElementById('signupNameInput').value.trim();
    const mobileNumber = document.getElementById('signupPhoneInput').value.trim();

    if (!validateInputs(name, mobileNumber)) return;

    const users = await fetchUsers();
    const existingUser = users.find(user => user.mobileNumber === mobileNumber);

    if (existingUser) {
        showAlert("Mobile number is already registered.");
        return;
    }

    try {
        const signupResponse = await fetch(USER_API_URL, {
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
        showAlert('An error occurred while signing up.');
    }
}

// Validate admin login
async function validateAdmin(event) {
    event.preventDefault();

    const userName = document.getElementById('adminNameInput').value.trim();
    const password = document.getElementById('adminPasswordInput').value.trim();

    if (!validateInputs(userName, password)) return;

    const admins = await fetchAdmins();
    const existingAdmin = admins.find(admin => admin.userName === userName && admin.password === password);

    if (existingAdmin) {
        localStorage.setItem("adminToken", existingAdmin.id); // Use Id instead of adminId
        showAlert('Admin login successful!');
        window.location.href = 'admin.html';
    } else {
        showAlert('Access denied. Invalid admin credentials.');
    }
}

// Toggle button event listeners
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
});