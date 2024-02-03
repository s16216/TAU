from selenium import webdriver
from selenium.webdriver.common.action_chains import ActionChains
from random import randrange


class ReservedTest:
    def __init__(self, first_name, last_name, email, password, driver):
        self.driver = driver
        self.first_name = first_name
        self.last_name = last_name
        self.email = email
        self.password = password

    def store_selector_shown(self):
        driver = self.driver
        driver.get("https://www.reserved.com/")
        header_text = driver.find_element_by_xpath('/html/body/main/header').text

        if (header_text != "CHOOSE YOUR COUNTRY"):
            print('NIE ZGADZA SIE TEXT')

    def register_user(self):
        driver = self.driver
        action = ActionChains(driver)
        driver.get("https://www.reserved.com/pl/pl/")
        account_menu = driver.find_element_by_xpath('//*[@id="headerWrapper"]/div/div[2]/button[1]')
        action.move_to_element(account_menu).perform()

        register_button = driver.find_element_by_xpath('//*[@id="headerWrapper"]/div/div[2]/div[2]/section[2]/a')
        register_button.click()

        driver.find_element_by_id("email_id").send_keys(self.email)
        driver.find_element_by_id("firstname_id").send_keys(self.first_name)
        driver.find_element_by_id("lastname_id").send_keys(self.last_name)
        driver.find_element_by_id("password_id").send_keys(self.password)
        driver.find_element_by_xpath('//*[@id="loginRegisterRoot"]/div/div[2]/div/form/button').click()

    def login_user(self):
        driver = self.driver
        action = ActionChains(driver)
        driver.get("https://www.reserved.com/")
        store_poland_button = driver.find_element_by_link_text('Polska / Poland')
        store_poland_button.click()
        account_menu = driver.find_element_by_xpath('//*[@id="headerWrapper"]/div/div[2]/button[1]')
        action.move_to_element(account_menu).perform()

        driver.find_element_by_xpath('//*[@id="headerWrapper"]/div/div[2]/div[2]/section[1]/a').click()

        driver.find_element_by_id("login[username]_id").send_keys(self.email)
        driver.find_element_by_id("login[password]_id").send_keys(self.password)
        driver.find_element_by_xpath('//*[@id="loginRegisterRoot"]/div/div[1]/div/form/button').click()

    def logout_user(self):
        driver = self.driver
        action = ActionChains(driver)
        driver.get("https://www.reserved.com/pl/pl")

        # newsletter_popup = driver.find_element_by_id('newsletterContainer')
        # if newsletter_popup:
        #     driver.find_element_by_xpath('//*[@id="newsletterContainer"]/div[1]').click()

        account_menu = driver.find_element_by_xpath('//*[@id="headerWrapper"]/div/div[2]/button[1]')
        action.move_to_element(account_menu).perform()

        logout_button = driver.find_element_by_xpath('//*[@id="headerWrapper"]/div/div[2]/div[2]/section[1]/div/a')
        logout_button.click()


class MediaMarkt:

    def __init__(self, driver):
        self.driver = driver

    def cart_is_empty_at_start(self):
        driver = self.driver
        driver.get("https://mediamarkt.pl/")
        driver.find_element_by_xpath('//*[@id="js-m-quickCartInfo"]/div/a').click()
        empty_cart_text = driver.find_element_by_xpath('//*[@id="js-cart-list-content"]/div/section/div/div[1]').text
        if (empty_cart_text != "Twój koszyk jest pusty."):
            print('NIE ZGADZA SIE TEXT')

    def can_back_from_empty_cart(self):
        driver = self.driver
        driver.get('https://mediamarkt.pl/koszyk/lista')
        driver.find_element_by_xpath('//*[@id="js-cart-list-content"]/div/section/div/div[2]/a').click()


drivers = [webdriver.Chrome(), webdriver.Firefox(),]

for d in drivers:
    email = "testowyemail" + str(int(randrange(5000))) + "@gmail.com"
    re = ReservedTest('tomasz', 'sosinski', email, 'Testowe123', d)
    try:
        re.store_selector_shown()
        re.register_user()
        re.logout_user()
        re.login_user()

        wy = MediaMarkt(d)
        wy.cart_is_empty_at_start()
        wy.can_back_from_empty_cart()
    except:
        print("An exception occurred")
