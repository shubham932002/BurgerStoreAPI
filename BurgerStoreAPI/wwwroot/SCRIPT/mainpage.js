const API_URL = 'https://localhost:7070/api/users';
const MOBILE_NUMBER_REGEX = /^\d{10}$/;

function showAlert(message) {

    alert(message);
}

//function validateInputs(name, mobileNumber) {
//    if (!name || !mobileNumber) {
//        showAlert("Both name and mobile number must be entered.");
//        return false;
//    }
//    if (!MOBILE_NUMBER_REGEX.test(mobileNumber)) {
//        showAlert("Mobile number must be 10 digits and in number format.");
//        return false;
//    }
//    return true;
//}

//async function fetchUsers() {
//    try {
//        const response = await fetch(API_URL);
//        if (!response.ok) throw new Error('Failed to fetch users');
//        return await response.json();
//    } catch (error) {
//        console.error('Error fetching users:', error);
//        showAlert('An error occurred while fetching users.');
//        return [];
//    }
//}

//async function validateUser(event) {
//    event.preventDefault();

//    const name = document.getElementById('nameInput').value.trim();
//    const mobileNumber = document.getElementById('phoneInput').value.trim();

//    if (!validateInputs(name, mobileNumber)) return;

//    const users = await fetchUsers();
//    const existingUser = users.find(user => user.name === name && user.mobileNumber === mobileNumber);


//    if (existingUser) {
//        localStorage.setItem("userToken", existingUser.userId);
//        showAlert('Welcome ' + name + '!');
//        window.location.href = 'menupage.html';
//    }
//    else if (adminUser)
//    {
//        localStorage.setItem("userToken", existingUser.userId);
//        showAlert('Admin Login Access Given - Welcome ' + name + '!');
//        window.location.href = 'burgers.html';

//    }
//    else {
//        showAlert('Access denied. Invalid user credentials.');
//    }
//}

//async function signupUser(event) {
//    event.preventDefault();

//    const name = document.getElementById('signupNameInput').value.trim();
//    const mobileNumber = document.getElementById('signupPhoneInput').value.trim();

//    if (!validateInputs(name, mobileNumber)) return;

//    const users = await fetchUsers();
//    const existingUser = users.find(user => user.mobileNumber === mobileNumber);

//    if (existingUser) {
//        showAlert("Mobile number is already registered.");
//        return;
//    }

//    try {
//        const signupResponse = await fetch(API_URL, {
//            method: 'POST',
//            headers: {
//                'Content-Type': 'application/json'
//            },
//            body: JSON.stringify({ name, mobileNumber })
//        });

//        if (signupResponse.ok) {
//            showAlert('Signup successful! You can now log in.');
//            document.getElementById('signupForm').reset();
//        } else {
//            showAlert('Signup failed. Please try again.');
//        }
//    } catch (error) {
//        console.error('Error during signup:', error);
//        showAlert('An error occurred while signing up.');
//    }
//}


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

async function fetchUsers() {
    try {
        const response = await fetch(API_URL);
        if (!response.ok) throw new Error('Failed to fetch users');
        return await response.json();
    } catch (error) {
        console.error('Error fetching users:', error);
        showAlert('An error occurred while fetching users.');
        return [];
    }
}

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
        const signupResponse = await fetch(API_URL, {
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

async function validateAdmin(event) {
    event.preventDefault();

    const name = document.getElementById('adminNameInput').value.trim();
    const mobileNumber = document.getElementById('adminPhoneInput').value.trim();

    if (!validateInputs(name, mobileNumber)) return;

    // Add admin login logic here
    if (name === 'admin' && mobileNumber === '0000000000') {
        localStorage.setItem("adminToken", 'admin');
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

