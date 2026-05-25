# Polynomials-and-Physics-Simulator
This was my project for school, it is a really cool app with an integrated database. You log in, start drawing your equations, and there is also a part where you can experiment with ball physics. I had a lot of fun building this and you can actually learn a lot from it.

# Basic ideas and functionality
We have a login page at the start. Once we register or login, we enter. In the graphing part, we enter a polynomial term in any non-simplified order, and the program will simplify the polynomial, order it, and graph it. If it is not already added, it will add it to your polynomial table in your profile and you can graph it again in the future. And if the polynomial is a fraction, just for fun, it will draw the graph it approaches for x going to infinity.

<img width="1842" height="987" alt="Drawing" src="https://github.com/user-attachments/assets/e87c9278-5d4b-49d1-a7da-4378c1f14234" />


The second section works the same way, but it is for ball physics. You enter 5 numbers for each ball, coordinates, vx, vy and radius, you start the application and the balls start colliding. Then, again, we modify the database with the corresponding dynamic SQL queries and in the future you can select to generate the balls you have already entered.
<img width="1877" height="947" alt="Balls" src="https://github.com/user-attachments/assets/d15c388b-a36d-4095-b101-590292292e96" />



# How to set up
You use the same code for the classes. For the database and the dynamic queries, sadly you will have to replace the places where I use my database and file connections with your specific ones. But it in the future I will upload a program that modifies a text (code) file and replaces one word with another at each occurence, it will only be manual for now. For the design, it is actually made to work with any PC screen, as it takes the percantage of the width and heigth of the screen, but you can modify the percentages for position x and y for the certain labels or buttons if you like.
#
The files and code are sadly quite lengthy and are not really made for an outside person to just look and understand everything that is going on, but if you are interested, you can learn more from the documentation I have attached. It is in Bulgarian, but you can very easily traslate it. The Pol class is not really explained, that one takes the string expression for the polynomial and uses Stack/recursion to analyze the entire string with brackets and priority, but it is REALLY messy. I do not have documentation for it yet, but you just need to trust that it works.
