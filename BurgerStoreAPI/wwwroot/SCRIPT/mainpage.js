const API_URL = 'https://localhost:7070/api/users';
const MOBILE_NUMBER_REGEX = /^\d{10}$/;

function showAlert(message) {
    // Replace alert with a more user-friendly notification system if available
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



//async function validateUser(event) {
//    event.preventDefault();

//    const name = document.getElementById('nameInput').value.trim();
//    const mobileNumber = document.getElementById('phoneInput').value.trim();

//    // Hide the error message initially
//    document.getElementById('errorMessage').style.display = 'none';

//    // Check if both name and mobile number are entered
//    if (!name || !mobileNumber) {
//        document.getElementById('errorMessage').textContent = 'Both name and mobile number must be entered.';
//        document.getElementById('errorMessage').style.display = 'block';
//        return;
//    }

//    // Validate mobile number format
//    if (!/^\d{10}$/.test(mobileNumber)) {
//        document.getElementById('errorMessage').textContent = 'Mobile number must be 10 digits.';
//        document.getElementById('errorMessage').style.display = 'block';
//        return;
//    }

//    try {
//        const response = await fetch('https://localhost:7070/api/users');
//        const users = await response.json();

//        const existingUser = users.find(user => user.name === name && user.mobileNumber === mobileNumber);

//        if (existingUser) {
//            const idtoken = existingUser.userId;
//            localStorage.setItem("userToken", idtoken);
//            alert('Welcome ' + name + '!');
//            window.location.href = 'menupage.html';
//        } else {
//            alert('Access denied. Invalid user credentials.');
//        }
//    } catch (error) {
//        console.error('Error:', error);
//        alert('An error occurred while validating the user.');
//    }
//}

//async function signupUser(event) {
//    event.preventDefault();

//    const name = document.getElementById('signupNameInput').value.trim();
//    const mobileNumber = document.getElementById('signupPhoneInput').value.trim();

//    // Hide the error message initially
//    document.getElementById('signupErrorMessage').style.display = 'none';

//    // Check if both name and mobile number are entered
//    if (!name || !mobileNumber) {
//        document.getElementById('signupErrorMessage').textContent = 'Both name and mobile number must be entered.';
//        document.getElementById('signupErrorMessage').style.display = 'block';
//        return;
//    }

//    // Validate mobile number format
//    if (!/^\d{10}$/.test(mobileNumber)) {
//        document.getElementById('signupErrorMessage').textContent = 'Mobile number must be 10 digits.';
//        document.getElementById('signupErrorMessage').style.display = 'block';
//        return;
//    }

//    try {
//        // Check if the mobile number is already taken
//        const response = await fetch('https://localhost:7070/api/users');
//        const users = await response.json();

//        const existingUser = users.find(user => user.mobileNumber === mobileNumber);

//        if (existingUser) {
//            document.getElementById('signupErrorMessage').textContent = 'Mobile number is already registered.';
//            document.getElementById('signupErrorMessage').style.display = 'block';
//            return;
//        }

//        // Proceed with signup
//        const signupResponse = await fetch('https://localhost:7070/api/users', {
//            method: 'POST',
//            headers: {
//                'Content-Type': 'application/json'
//            },
//            body: JSON.stringify({
//                name: name,
//                mobileNumber: mobileNumber
//            })
//        });

//        if (signupResponse.ok) {
//            alert('Signup successful! You can now log in.');
//            document.getElementById('signupForm').reset();
//        } else {
//            alert('Signup failed. Please try again.');
//        }
//    } catch (error) {
//        console.error('Error:', error);
//        alert('An error occurred while signing up.');
//    }
//}