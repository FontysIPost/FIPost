describe('Update the city', () => {
    it('Updates the city and reverts it', () => {
        cy.visit('http://localhost:8081');

        cy.get('.input-container').first()
            .type('a@a.nl')
            .next().type('a')
        cy.contains('Inloggen').click()

        cy.reload();

        cy.contains('Locaties').click();

        cy.contains('Steden').click();

        cy.contains('Waalwijk').click();

        cy.get('.input').clear();

        cy.contains('Bevestigen').click();

        cy.contains('Niet alle velden zijn ingevoerd');

        cy.get('.input').type('Wolwijk');

        cy.contains('Bevestigen').click();

        cy.contains('Delete').should('not.exist');

        cy.contains('Wolwijk').click();

        cy.get('.input').clear();

        cy.get('.input').type('Waalwijk');

        cy.contains('Bevestigen').click();

        cy.contains('Waalwijk');
    })
});