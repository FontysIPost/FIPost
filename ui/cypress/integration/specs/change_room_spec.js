describe('Update city/building of room', () => {
    it('Updates the city/building of a room', () => {
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

        cy.get('input.input').eq(1).click();

        cy.get('input.input').eq(1).click().then(option => {
            cy.wrap(option).siblings().contains('Test').click();
        })

        cy.get('input.input').eq(1).should('have.value', 'Test');

        cy.contains('Bevestigen').click();

        cy.contains('Delete').should('not.exist');

        cy.contains('0.13').click();

        cy.get('input.input').first().click();

        cy.get('input.input').first().click().then(option => {
            cy.wrap(option).siblings().contains('Tilburg').click();
        });

        cy.get('input.input').first().should('have.value', 'Tilburg');

        cy.get('input.input').eq(1).should('have.value', '');

        cy.get('input.input').eq(1).click().then(option => {
            cy.wrap(option).siblings().contains('P8').click();
        })

        cy.get('input.input').eq(1).should('have.value', 'P8');

        cy.contains('Bevestigen').click();

        cy.contains('Delete').should('not.exist');

        cy.contains('0.13').siblings().contains('P8');
    })
});