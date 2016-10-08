from django.shortcuts import render
from django.http import HttpResponse

import psycopg2
import sys

def index(request):
	params = {
		'database': 'greyraven',
		'user': 'postgres',
		'password': 'emc921l',
		'host': '127.0.0.1',
		'port': '5432'
	}
	con = psycopg2.connect(**params)
	cur = con.cursor()
	cur.execute("SELECT * FROM people")
	rows = cur.fetchall()
	
	context = {
		'rows' : rows,
	}
	return HttpResponse(render(request, 'dashboard/index.html', context))
