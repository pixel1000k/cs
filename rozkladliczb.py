input = int(input())
print("-")
i = 2
while input != 1:
    while i <= input:
        if input % i == 0:
            input = input / i
            print(i)
            break
        i += 1