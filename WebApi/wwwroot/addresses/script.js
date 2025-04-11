const API_URL = 'https://localhost:7293/api/address';

document.getElementById('listAddressesButton').addEventListener('click', async () => {
    try {
        const response = await fetch(API_URL);
        const addresses = await response.json();

        const addressesList = document.getElementById('addressesList');
        addressesList.innerHTML = '<h2>Liste des adresses</h2>';

        addresses.forEach(address => {
            const addressDiv = document.createElement('div');
            addressDiv.className = 'address';
            addressDiv.innerHTML = `
                <h3>Adresse ID: ${address.addressID}</h3>
                <p><span>Rue:</span> ${address.street}</p>
                <p><span>Code postal:</span> ${address.zipCode}</p>
                <p><span>Ville:</span> ${address.city}</p>
                <p><span>Pays:</span> ${address.country}</p>
                <p><span>Client ID:</span> ${address.clientID}</p>
            `;
            addressesList.appendChild(addressDiv);
        });
    } catch (error) {
        console.error('Erreur lors du chargement des adresses :', error);
    }
});

document.getElementById('getSpecificAddressButton').addEventListener('click', async () => {
    const id = document.getElementById('specificAddressID').value;

    if (!id) {
        alert('Veuillez entrer un ID valide.');
        return;
    }

    try {
        const response = await fetch(`${API_URL}/${id}`);
        if (!response.ok) {
            throw new Error('Adresse non trouvée');
        }

        const address = await response.json();

        const specificAddress = document.getElementById('specificAddress');
        specificAddress.innerHTML = `
            <h2>Détails de l'adresse</h2>
            <div class="address">
                <h3>Adresse ID: ${address.addressID}</h3>
                <p><span>Rue:</span> ${address.street}</p>
                <p><span>Code postal:</span> ${address.zipCode}</p>
                <p><span>Ville:</span> ${address.city}</p>
                <p><span>Pays:</span> ${address.country}</p>
                <p><span>Client ID:</span> ${address.clientID}</p>
            </div>
        `;
    } catch (error) {
        console.error('Erreur lors de la récupération de l\'adresse :', error);
        alert('Adresse introuvable.');
    }
});

document.getElementById('createAddressForm').addEventListener('submit', async (e) => {
    e.preventDefault();

    const address = {
        street: document.getElementById('street').value,
        zipCode: document.getElementById('zipCode').value,
        city: document.getElementById('city').value,
        country: document.getElementById('country').value,
        clientID: parseInt(document.getElementById('clientID').value, 10),
    };

    try {
        const response = await fetch(API_URL, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(address),
        });

        if (response.ok) {
            alert('Adresse créée avec succès !');
            document.getElementById('createAddressForm').reset();
        } else {
            throw new Error('Erreur lors de la création de l\'adresse');
        }
    } catch (error) {
        console.error(error);
        alert('Impossible de créer l\'adresse.');
    }
});

document.getElementById('updateAddressForm').addEventListener('submit', async (e) => {
    e.preventDefault();

    const id = parseInt(document.getElementById('updateAddressID').value, 10);
    const updatedAddress = {
        addressID: id,
        street: document.getElementById('updateStreet').value || null,
        zipCode: document.getElementById('updateZipCode').value || null,
        city: document.getElementById('updateCity').value || null,
        country: document.getElementById('updateCountry').value || null,
        clientID: parseInt(document.getElementById('updateClientID').value, 10) || null,
    };

    try {
        const response = await fetch(`${API_URL}/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(updatedAddress),
        });

        if (response.ok) {
            alert('Adresse modifiée avec succès !');
            document.getElementById('updateAddressForm').reset();
        } else {
            throw new Error('Erreur lors de la modification de l\'adresse');
        }
    } catch (error) {
        console.error(error);
        alert('Impossible de modifier l\'adresse.');
    }
});

document.getElementById('deleteAddressForm').addEventListener('submit', async (e) => {
    e.preventDefault();

    const id = document.getElementById('deleteAddressID').value;

    try {
        const response = await fetch(`${API_URL}/${id}`, {
            method: 'DELETE',
        });

        if (response.ok) {
            alert('Adresse supprimée avec succès !');
            document.getElementById('deleteAddressForm').reset();
        } else {
            throw new Error('Erreur lors de la suppression de l\'adresse');
        }
    } catch (error) {
        console.error(error);
        alert('Impossible de supprimer l\'adresse.');
    }
});
