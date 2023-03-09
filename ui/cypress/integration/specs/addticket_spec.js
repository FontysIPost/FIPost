describe('Test if the user can add a ticket to the package', () => {
    it('Tests the add ticket function', () => {

        cy.visit('http://localhost:8081');

        cy.get('.input-container').first()
            .type('a@a.nl')
            .next().type('a')
        cy.contains('Inloggen').click()

        cy.reload();

        cy.contains('Zoeken').click();

        // Check to see if pagination works
        cy.get('thead>tr').children().contains('Naam').click();

        cy.contains('sdksk').click();

        cy.contains('0e90413a-ddb6-4277-5967-08d983e4ccbd');

        cy.get('.input').first().click().then(option => {
            cy.wrap(option).siblings().contains('Jaap van der Meer').click();
        });

        cy.get('.input').eq(1).click().then(option => {
            cy.wrap(option).siblings().contains('a1').click();
        });

        cy.contains('Toevoegen').click();

        cy.contains('a1');
    })
  })