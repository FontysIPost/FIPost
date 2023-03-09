describe('Correct Login', () => {
    it('Logs in with the right credentials', () => {
        cy.visit('http://localhost:8081');
        cy.get('.input-container').first()
            .type('a@a.nl')
            .next().type('a')
        cy.contains('Inloggen').click()
        cy.reload();
        cy.contains('Logout').click();
    })
});

describe('Incomplete credentials (no email) to Login', () => {
    it('Incomplete login credentials (no email)', () => {
        cy.visit('http://localhost:8081');
        cy.get('.input-container').first()
            .next().type('a')
        cy.contains('Inloggen').click()
        cy.contains('Niet alle velden zijn ingevuld.')
    })
});

describe('Incomplete credentials (no password) to Login', () => {
    it('Incomplete login credentials (no password)', () => {
        cy.visit('http://localhost:8081');
        cy.get('.input-container').first().type('a@a.nl')
        cy.contains('Inloggen').click()
        cy.contains('Niet alle velden zijn ingevuld.')
    })
});

describe('Incomplete credentials (no password) to Login', () => {
    it('Incomplete login credentials (no password)', () => {
        cy.visit('http://localhost:8081');
        cy.contains('Inloggen').click()
        cy.contains('Niet alle velden zijn ingevuld.')
    })
});

describe('Password and email do not match', () => {
    it('Password and email do not match', () => {
        cy.visit('http://localhost:8081');
        cy.get('.input-container').first()
            .type('b@a.nl')
            .next().type('b')
        cy.contains('Inloggen').click()
    })
});
