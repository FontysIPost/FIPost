const { faCarSide } = require("@fortawesome/free-solid-svg-icons");

describe('Test the add action to package', () => {
    it('Tests add action function', () => {

        cy.visit('http://localhost:8081');

        cy.get('.input-container').first()
            .type('a@a.nl')
            .next().type('a')
        cy.contains('Inloggen').click()

        cy.reload();

        cy.contains('Zoeken').click();
    })
})