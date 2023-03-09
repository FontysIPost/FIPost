describe('Update the building', () => {
    it('Updates the building and reverts it', () => {        
        cy.visit('http://localhost:8081');

        cy.get('.input-container').first()
            .type('a@a.nl')
            .next().type('a')
        cy.contains('Inloggen').click()

        cy.reload();

        cy.contains('Locaties').click();

        cy.contains('Gebouwen').click();

        cy.contains('Stadhuis').click();

        cy.contains('Wijzig een gebouw');

        cy.get('.selected').eq(1).click().then(option => {
            cy.wrap(option).siblings().contains('Wolluk').click();
        });

        cy.get('.input').first().clear();

        cy.contains('Bevestigen').click({force:true});

        cy.contains('Niet alle velden zijn correct ingevuld');

        cy.get('.input').first().type('Datssiuh');

        cy.get('.input').eq(1).clear().type('Nielpsiuhdats');

        cy.get('.input').eq(2).clear().type('404');

        cy.get('.input').eq(3).type('lul');

        cy.get('.input').eq(4).clear().type('4586MJ');

        cy.contains('Bevestigen').click({force:true});

        cy.contains('Delete').should('not.exist');

        cy.contains('Datssiuh').click();

        cy.contains('Wijzig een gebouw');

        cy.get('.selected').eq(1).click().then(option => {
            cy.wrap(option).siblings().contains('Woensel-Noord').click();
        });

        cy.get('.input').first().clear().type('Stadhuis');

        cy.get('.input').eq(1).clear().type('Stadhuisplein');

        cy.get('.input').eq(2).clear().type('5');

        cy.get('.input').eq(3).clear();

        cy.get('.input').eq(4).clear().type('6854JM');

        cy.contains('Bevestigen').click({force:true});

        cy.contains('Delete').should('not.exist');

        cy.contains('Stadhuis');
    })
});