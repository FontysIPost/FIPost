
describe('Login test succes & failure', () => {
    it('Has elements', () => {

        cy.visit('http://localhost:8081/#/login')

        cy.get('h1');

        cy.get('[type="email"]');

        cy.get('[type="password"]');

        cy.get('.small-btn-finish > div');
    })

    it('Login failure', () =>{

        cy.get('.small-btn-finish > div').click();

        cy.get('.error-text');

        cy.contains('Opnieuw');
    })

    it('Login succes', () =>{

        cy.reload();

        cy.get('[type="email"]').type('test@gmail.com').should('have.value', 'test@gmail.com');

        cy.get('[type="password"]').type('password').should('have.value', 'password');

        cy.get('.small-btn-finish > div').click();

        cy.get('.error-text').should('not.exist');

        cy.contains('Opnieuw').should('not.exist');
    })
});
describe('Login test succes & failure', () => {
    it('Has elements', () => { 

        cy.visit('http://localhost:8081/#/sign-up')

        cy.get('h1');

        cy.get('[type="email"]');

        cy.contains('Wachtwoord:');

        cy.contains('Herhaal wachtwoord:');

        cy.get('.small-btn-finish > div');
    })

    it('Sign-up failure', () =>{

        cy.get('.small-btn-finish > div').click();

        cy.contains('Niet alle velden zijn ingevuld');

        cy.get('.error-text');

        cy.contains('Opnieuw');

        cy.get('[type="email"]').type('test@gmail.com').should('have.value', 'test@gmail.com');

        cy.get('[type="password"]').first().type('password').should('have.value', 'password');

        cy.get('[type="password"]').last().type('pass').should('have.value', 'pass');

        cy.get('.small-btn-finish > div').click();

        cy.get('.error-text');

        cy.contains('Niet alle velden zijn ingevuld').should('not.exist');
       
        cy.contains('De ingevoerde wachtwoorden komen niet overeen');

        cy.contains('Opnieuw');
    })

    it('Email failure', () =>{

        cy.reload();

        cy.get('[type="email"]').type('testgmailcom').should('have.value', 'testgmailcom');

        cy.get('[type="password"]').first().type('password').should('have.value', 'password');

        cy.get('[type="password"]').last().type('password').should('have.value', 'password');

        cy.get('.small-btn-finish > div').click();

        cy.get('.error-text');

        cy.contains('Niet alle velden zijn ingevuld').should('not.exist');
       
        cy.contains('De ingevoerde wachtwoorden komen niet overeen').should('not.exist');     
        
        cy.get('[type="email"]').clear();

        cy.get('[type="email"]').type('test@gmailcom');

        cy.contains('Opnieuw').click();

        cy.get('.error-text');

    })

    it('Sign-up succes', () =>{

        cy.reload();

        cy.get('[type="email"]').type('test@gmail.com').should('have.value', 'test@gmail.com');

        cy.get('[type="password"]').first().type('password').should('have.value', 'password');

        cy.get('[type="password"]').last().type('password').should('have.value', 'password');

        cy.get('.small-btn-finish > div').click();

        cy.get('.error-text').should('not.exist');

        cy.contains('Niet alle velden zijn ingevuld').should('not.exist');
       
        cy.contains('De ingevoerde wachtwoorden komen niet overeen').should('not.exist');

        cy.contains('Opnieuw').should('not.exist');
    })
});