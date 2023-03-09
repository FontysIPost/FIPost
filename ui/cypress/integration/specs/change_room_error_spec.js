describe('Checks for error editing room', () => {
    it('Should give error', () => {
        cy.visit('http://localhost:8081');
        
        cy.get('.input-container').first()
            .type('a@a.nl')
            .next().type('a')
        cy.contains('Inloggen').click()

        cy.reload();

        cy.contains('Locaties').click();

        cy.contains('0.13').click();

        cy.get('input.input').first().click().then(option => {
            cy.wrap(option).siblings().contains('TestCity').click();
        });

        cy.get('input.input').first().should('have.value', 'TestCity');

        cy.get('input.input').eq(1).should('have.value', '');

        cy.contains('Bevestigen').click();

        cy.contains('dit gebouw bestaat niet');
    })
});