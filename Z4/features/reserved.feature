Feature: Reserved Website Testing

  Scenario: Verify store selector is shown
    Given the user opens the Reserved website
    Then the header text should not be "CHOOSE YOUR COUNTRY"

  Scenario: Register, login, and logout user on Reserved website
    Given the user opens the Reserved website
    When the user registers with valid details
    Then the user should be able to logout and login successfully

  Scenario: Verify MediaMarkt cart functionality
    Given the user opens the MediaMarkt website
    Then the cart should be empty at the start
    When the user tries to go back from the empty cart
    Then the user should be able to go back
