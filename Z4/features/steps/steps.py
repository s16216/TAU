from behave import given, when, then
from selenium import webdriver
from selenium.webdriver.common.action_chains import ActionChains
from random import randrange
from reserved_tests import ReservedTest, MediaMarkt

drivers = [webdriver.Chrome(), webdriver.Firefox(),]

@given('the user opens the Reserved website')
def open_reserved_website(context):
    context.driver = drivers[0]
    context.email = "testowyemail" + str(int(randrange(5000))) + "@gmail.com"
    context.reserved_test = ReservedTest('tomasz', 'sosinski', context.email, 'Testowe123', context.driver)
    context.reserved_test.store_selector_shown()

@when('the user registers with valid details')
def register_user(context):
    context.reserved_test.register_user()

@then('the user should be able to logout and login successfully')
def logout_and_login_user(context):
    context.reserved_test.logout_user()
    context.reserved_test.login_user()

@given('the user opens the MediaMarkt website')
def open_mediamarkt_website(context):
    context.driver = drivers[1]
    context.mediamarkt = MediaMarkt(context.driver)
    context.mediamarkt.cart_is_empty_at_start()

@then('the cart should be empty at the start')
def verify_empty_cart(context):
    context.mediamarkt.cart_is_empty_at_start()

@when('the user tries to go back from the empty cart')
def go_back_from_empty_cart(context):
    context.mediamarkt.can_back_from_empty_cart()

@then('the user should be able to go back')
def verify_go_back(context):
    pass
